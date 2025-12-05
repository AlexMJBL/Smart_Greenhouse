using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class PlantHumidityRecordService : IRepository<PlantHumidityRecord, int>
    {
        private SerreContext _context;
        private readonly ILogger<PlantHumidityRecordService> _logger;

        private readonly IRepository<Plant, int> _plantService;
        private readonly IRepository<PlantAlert, int> _plantAlertService;
        private readonly IRepository<Specimen, int> _specimenService;
        private readonly IRepository<SoilHumidityCategory, int> _soilHumidityCategoryService;

        public PlantHumidityRecordService(SerreContext context, ILogger<PlantHumidityRecordService> logger, 
        IRepository<Plant, int> plantService, IRepository<PlantAlert, int> plantAlertService,
        IRepository<Specimen, int> specimenService, IRepository<SoilHumidityCategory, int> soilHumidityCategoryService)
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
           return await _context.PlantHumidityRecords.ToListAsync();
        }

        public async Task<IEnumerable<PlantHumidityRecord>> GetAllWithFilter(Expression<Func<PlantHumidityRecord, bool>>? filter = null)
        {
            IQueryable<PlantHumidityRecord> query = _context.PlantHumidityRecords;

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<PlantHumidityRecord?> GetByIdAsync(int id)
        {
            return await _context.PlantHumidityRecords.FindAsync(id);
        }
        
        public async Task<PlantHumidityRecord> AddAsync(PlantHumidityRecord plantHumidityRecord)
        {

            var plant = await _plantService.GetByIdAsync(plantHumidityRecord.PlantId);
            if (plant == null)
            {
                throw new ArgumentException($"Plant with ID {plantHumidityRecord.PlantId} does not exist.");
            }

            var specimen = await _specimenService.GetByIdAsync(plant.SpecimenId);
            if (specimen == null)
            {
                throw new ArgumentException($"Specimen with ID {plant.SpecimenId} does not exist.");
            }   

            var soilHumidityCategory = await _soilHumidityCategoryService.GetByIdAsync(specimen.SoilHumidityCatId);
            if (soilHumidityCategory == null)
            {
                throw new ArgumentException($"SoilHumidityCategory with ID {specimen.SoilHumidityCatId} does not exist.");
            }

            if(plantHumidityRecord.RecordPct < soilHumidityCategory.MinHumidityPct || plantHumidityRecord.RecordPct > soilHumidityCategory.MaxHumidityPct)
            {
                var reason = plantHumidityRecord.RecordPct < soilHumidityCategory.MinHumidityPct
                    ? $"Humidity too low: {plantHumidityRecord.RecordPct}% (min: {soilHumidityCategory.MinHumidityPct}%)"
                    : $"Humidity too high: {plantHumidityRecord.RecordPct}% (max: {soilHumidityCategory.MaxHumidityPct}%)";

                var alert = new PlantAlert
                {
                    PlantId = plantHumidityRecord.PlantId,
                    Reason = reason,
                    CreatedAt = DateTime.Now
                };
                await _plantAlertService.AddAsync(alert);
                _logger.LogInformation("Created PlantAlert for Plant ID {PlantId} due to humidity record {RecordPct}%", plantHumidityRecord.PlantId, plantHumidityRecord.RecordPct);
            }

            _context.PlantHumidityRecords.Add(plantHumidityRecord);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Added new PlantHumidityRecord with ID {Id}", plantHumidityRecord.Id);
            return plantHumidityRecord;
        }
        public async Task<PlantHumidityRecord> UpdateAsync(int id, PlantHumidityRecord plantHumidityRecord)
        {
            var plant = await _plantService.GetByIdAsync(plantHumidityRecord.PlantId);
            if (plant == null)
            {
                throw new ArgumentException($"Plant with ID {plantHumidityRecord.PlantId} does notexist.");
            } 

            if (id != plantHumidityRecord.Id)
            {
                _logger.LogError("PlantHumidityRecord ID mismatch: {Id} != {PlantHumidityRecordId}", id, plantHumidityRecord.Id);
                throw new ArgumentException("PlantHumidityRecord ID mismatch.");
            }

            _context.PlantHumidityRecords.Update(plantHumidityRecord);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated PlantHumidityRecord with ID {Id}", plantHumidityRecord.Id);
            return plantHumidityRecord;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var plantHumidityRecord = await GetByIdAsync(id);
            if (plantHumidityRecord == null)
                return false;
            
            _context.PlantHumidityRecords.Remove(plantHumidityRecord);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted PlantHumidityRecord with ID {Id}", id);
            return true;
        }
    }
}
