using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class PlantHumidityRecordService : IRepository<PlantHumidityRecord, int>
    {
        private readonly SerreContext _context;
        private readonly ILogger<PlantHumidityRecordService> _logger;

        private readonly IRepository<Plant, int> _plantService;
        private readonly IRepository<PlantAlert, int> _plantAlertService;
        private readonly IRepository<Specimen, int> _specimenService;
        private readonly IRepository<SoilHumidityCategory, int> _soilHumidityCategoryService;

        public PlantHumidityRecordService(
            SerreContext context,
            ILogger<PlantHumidityRecordService> logger,
            IRepository<Plant, int> plantService,
            IRepository<PlantAlert, int> plantAlertService,
            IRepository<Specimen, int> specimenService,
            IRepository<SoilHumidityCategory, int> soilHumidityCategoryService)
        {
            _context = context;
            _logger = logger;
            _plantService = plantService;
            _plantAlertService = plantAlertService;
            _specimenService = specimenService;
            _soilHumidityCategoryService = soilHumidityCategoryService;
        }

        public async Task<IEnumerable<PlantHumidityRecord>> GetAllAsync()
        {
            return await _context.PlantHumidityRecords
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<PlantHumidityRecord>> GetAllWithFilter(Expression<Func<PlantHumidityRecord, bool>>? filter = null)
        {
            IQueryable<PlantHumidityRecord> query = _context.PlantHumidityRecords.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<PlantHumidityRecord?> GetByIdAsync(int id)
        {
            return await _context.PlantHumidityRecords
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PlantHumidityRecord> AddAsync(PlantHumidityRecord plantHumidityRecord)
        {
            await ValidateHumidityAndGenerateAlertAsync(plantHumidityRecord);

            _context.PlantHumidityRecords.Add(plantHumidityRecord);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Added new PlantHumidityRecord with ID {Id}", plantHumidityRecord.Id);
            return plantHumidityRecord;
        }

        public async Task<PlantHumidityRecord> UpdateAsync(int id, PlantHumidityRecord plantHumidityRecord)
        {
            if (id != plantHumidityRecord.Id)
            {
                _logger.LogError("PlantHumidityRecord ID mismatch: {Id} != {RecordId}", id, plantHumidityRecord.Id);
                throw new ArgumentException("PlantHumidityRecord ID mismatch.");
            }

            await ValidateHumidityAndGenerateAlertAsync(plantHumidityRecord);

            _context.PlantHumidityRecords.Update(plantHumidityRecord);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Updated PlantHumidityRecord with ID {Id}", plantHumidityRecord.Id);
            return plantHumidityRecord;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await _context.PlantHumidityRecords.FindAsync(id);
            if (record == null)
                return false;

            _context.PlantHumidityRecords.Remove(record);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Deleted PlantHumidityRecord with ID {Id}", id);
            return true;
        }
        
        private async Task ValidateHumidityAndGenerateAlertAsync(PlantHumidityRecord record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            // Plant
            var plant = await _plantService.GetByIdAsync(record.PlantId)
                ?? throw new ArgumentException($"Plant with ID {record.PlantId} does not exist.");

            // Specimen
            var specimen = await _specimenService.GetByIdAsync(plant.SpecimenId)
                ?? throw new ArgumentException($"Specimen with ID {plant.SpecimenId} does not exist.");

            // Humidity Category
            var soilCategory = await _soilHumidityCategoryService.GetByIdAsync(specimen.SoilHumidityCatId)
                ?? throw new ArgumentException($"SoilHumidityCategory with ID {specimen.SoilHumidityCatId} does not exist.");

            // Check humidity
            if (record.RecordPct < soilCategory.MinHumidityPct || record.RecordPct > soilCategory.MaxHumidityPct)
            {
                var reason = record.RecordPct < soilCategory.MinHumidityPct
                    ? $"Humidity too low: {record.RecordPct}% (min: {soilCategory.MinHumidityPct}%)"
                    : $"Humidity too high: {record.RecordPct}% (max: {soilCategory.MaxHumidityPct}%)";

                var alert = new PlantAlert
                {
                    PlantId = record.PlantId,
                    Reason = reason,
                    CreatedAt = DateTime.UtcNow
                };

                await _plantAlertService.AddAsync(alert);

                _logger.LogWarning("Created PlantAlert for Plant ID {PlantId}: {Reason}", record.PlantId, reason);
            }
        }
    }
}