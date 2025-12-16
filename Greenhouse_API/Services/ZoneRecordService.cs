using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Greenhouse_API.Services;

namespace Greenhouse_API.Services
{
    public class ZoneRecordService : IRepository<ZoneRecord, int>
    {
        private SerreContext _context;
        private readonly ILogger<ZoneRecordService> _logger;
        private readonly IRepository<ZoneCategory, int> _zoneCategoryService;
        private readonly IRepository<Zone, int> _zoneService;
        private readonly IRepository<ZoneSensorAlert, int> _zoneAlertService;

        public ZoneRecordService(SerreContext context, ILogger<ZoneRecordService> logger,
         IRepository<ZoneCategory, int> zoneCategoryService, IRepository<Zone, int> zoneService, IRepository<ZoneSensorAlert, int> zoneAlertService)
        {
            _context = context;
            _logger = logger;
            _zoneCategoryService = zoneCategoryService;
            _zoneService = zoneService;
            _zoneAlertService = zoneAlertService;
        }

        public async Task<IEnumerable<ZoneRecord>> GetAllAsync()
        {
            return await _context.ZoneRecords.ToListAsync();
        }

        public async Task<IEnumerable<ZoneRecord>> GetAllWithFilter(Expression<Func<ZoneRecord, bool>>? filter = null)
        {
            IQueryable<ZoneRecord> query = _context.ZoneRecords;

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<ZoneRecord?> GetByIdAsync(int id)
        {
            return await _context.ZoneRecords.FindAsync(id);
        }

  public async Task<ZoneRecord> AddAsync(ZoneRecord zoneRecord)
{
    await ValidateRecordAndGenerateAlertAsync(zoneRecord);

    _context.ZoneRecords.Add(zoneRecord);
    await _context.SaveChangesAsync();

    _logger.LogInformation($"ZoneRecord with ID {zoneRecord.Id} added successfully.");
    return zoneRecord;
}

    public async Task<ZoneRecord> UpdateAsync(int id, ZoneRecord zoneRecord)
{
    if (id != zoneRecord.Id)
        throw new ArgumentException("ID mismatch between route and body.");

    await ValidateRecordAndGenerateAlertAsync(zoneRecord);

    _context.ZoneRecords.Update(zoneRecord);
    await _context.SaveChangesAsync();

    _logger.LogInformation($"ZoneRecord with ID {zoneRecord.Id} updated successfully.");
    return zoneRecord;
}

        public async Task<bool> DeleteAsync(int id)
        {
            var zoneRecord = await _context.ZoneRecords.FindAsync(id);
            if (zoneRecord == null)
            {
                return false;
            }

            _context.ZoneRecords.Remove(zoneRecord);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"ZoneRecord with ID {id} deleted successfully.");
            return true;
        }

        private async Task<ZoneCategory> getZoneCategoryAsync(ZoneRecord record)
{
    var zone = await _zoneService.GetByIdAsync(record.ZoneId)
        ?? throw new ArgumentException($"Zone with ID {record.ZoneId} does not exist.");

    var zoneCategory = await _zoneCategoryService.GetByIdAsync(zone.ZoneCategoryId)
        ?? throw new ArgumentException($"ZoneCategory with ID {zone.ZoneCategoryId} does not exist.");
        
    return zoneCategory;
}

private async Task ValidateRecordAndGenerateAlertAsync(ZoneRecord zoneRecord)
{
    var zoneCategory = await getZoneCategoryAsync(zoneRecord);

    bool inRange = zoneRecord.Type.ToLower() switch
    {
        "humidity" => zoneRecord.Record >= zoneCategory.HumidityMinPct && zoneRecord.Record <= zoneCategory.HumidityMaxPct,
        "temperature" => zoneRecord.Record >= zoneCategory.TemperatureMinC && zoneRecord.Record <= zoneCategory.TemperatureMaxC,
        "luminosity" => zoneRecord.Record >= zoneCategory.LuminosityMinLux && zoneRecord.Record <= zoneCategory.LuminosityMaxLux,
        _ => throw new ArgumentException($"Unknown record type: {zoneRecord.Type}")
    };

    zoneRecord.InRange = inRange;

    if (!inRange)
    {
        var alert = new ZoneSensorAlert
        {
            ZoneId = zoneRecord.ZoneId,
            Message = $"{zoneRecord.Type} out of range: {zoneRecord.Record}",
            CreatedAt = DateTime.UtcNow
        };

        await _zoneAlertService.AddAsync(alert);
        _logger.LogWarning($"Generated alert for Zone ID {zoneRecord.ZoneId}: {alert.Message}");
    }
}
    
    }
}   