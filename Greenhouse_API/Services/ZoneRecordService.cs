using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Greenhouse_API.Services;
using Greenhouse_API.DTOs;

namespace Greenhouse_API.Services
{
    public class ZoneRecordService : IZoneRecordService
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

        public Task<IEnumerable<ZoneRecordDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ZoneRecordDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ZoneRecordDto> CreateAsync(ZoneRecordWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ZoneRecordDto> UpdateAsync(int id, ZoneRecordWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}   