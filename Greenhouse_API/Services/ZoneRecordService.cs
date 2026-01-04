using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Greenhouse_API.Services;
using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Enums;
using System.Runtime.InteropServices;

namespace Greenhouse_API.Services
{
    public class ZoneRecordService : IZoneRecordService
    {
        private IRepository<ZoneRecord> _repository;
        private readonly ILogger<ZoneRecordService> _logger;
        private readonly IZoneCategoryService _zoneCategoryService;
        private readonly IZoneService _zoneService;
        private readonly IZoneSensorAlertService _zoneAlertService;
        private readonly ISensorService _sensorService;

        public ZoneRecordService(IRepository<ZoneRecord> repository, ILogger<ZoneRecordService> logger,
         IZoneCategoryService zoneCategoryService, IZoneService zoneService, IZoneSensorAlertService zoneAlertService, ISensorService sensorService)
        {
            _repository = repository;
            _logger = logger;
            _zoneCategoryService = zoneCategoryService;
            _zoneService = zoneService;
            _zoneAlertService = zoneAlertService;
            _sensorService = sensorService;
        }

        public async Task<IEnumerable<ZoneRecordDto>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all zone records");
            var zoneRecords = await _repository.GetAllAsync();

            return zoneRecords.Select(zr => new ZoneRecordDto
            {
                Id = zr.Id,
                Record = zr.Record,
                InRange = zr.InRange,
                ZoneId = zr.ZoneId,
                SensorId = zr.SensorId,
                Type = zr.Type,
                CreatedAt = zr.CreatedAt
            });
        }

        public async Task<ZoneRecordDto?> GetByIdAsync(int id)
        {
            var zoneRecord = await _repository.GetByIdAsync(id);
            if (zoneRecord == null)
            {
                _logger.LogWarning("ZoneRecord with ID: {ZoneRecordId} not found", id);
                return null;
            }

            _logger.LogInformation("ZoneRecord with ID: {ZoneRecordId} retrieved", id);
            return new ZoneRecordDto
            {
                Id = zoneRecord.Id,
                Record = zoneRecord.Record,
                InRange = zoneRecord.InRange,
                ZoneId = zoneRecord.ZoneId,
                SensorId = zoneRecord.SensorId,
                Type = zoneRecord.Type,
                CreatedAt = zoneRecord.CreatedAt
            };
        }

        public async Task<ZoneRecordDto> CreateAsync(ZoneRecordWriteDto dto)
        {
            var sensor = await _sensorService.GetByIdAsync(dto.SensorId);
            if (sensor == null)
            {
                _logger.LogWarning("Sensor with ID: {SensorId} not found for pressure record creation", dto.SensorId);
                throw new NotFoundException("Zone not found");
            }

            var zone = await _zoneService.GetByIdAsync(sensor.ZoneId);
            if (zone == null)
            {
                _logger.LogWarning("Zone with ID: {ZoneId} not found for pressure record creation", sensor.ZoneId);
                throw new NotFoundException("Zone not found");
            }

            var zoneRecord = new ZoneRecord
            {
                Record = dto.Record,
                ZoneId = sensor.ZoneId,
                SensorId = dto.SensorId,
                Type = dto.Type,
                CreatedAt = DateTime.UtcNow
            };

            await ValidateRecordAndGenerateAlertAsync(zoneRecord);
            await _repository.AddAsync(zoneRecord);
            _logger.LogInformation("ZoneRecord created with ID: {ZoneRecordId}", zoneRecord.Id);

            return new ZoneRecordDto
            {
                Id = zoneRecord.Id,
                Record = zoneRecord.Record,
                InRange = zoneRecord.InRange,
                ZoneId = zoneRecord.ZoneId,
                SensorId = zoneRecord.SensorId,
                Type = zoneRecord.Type,
                CreatedAt = zoneRecord.CreatedAt
            };
        }

        public async Task DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogWarning("ZoneRecord with ID {ZoneRecordId} not found for deletion", id);
                throw new NotFoundException("ZoneRecord not found");
            }

            _logger.LogInformation("ZoneRecord with ID {ZoneRecordId} deleted", id);
        }

        private async Task ValidateRecordAndGenerateAlertAsync(ZoneRecord zoneRecord)
        {
            var zone = await _zoneService.GetByIdAsync(zoneRecord.ZoneId)
                ?? throw new NotFoundException("Zone not found");

            var zoneCategory = await _zoneCategoryService.GetByIdAsync(zone.ZoneCategoryId)
                ?? throw new NotFoundException("ZoneCategory not found");

            double min;
            double max;

            switch (zoneRecord.Type)
            {
                case SensorType.humidity:
                    min = zoneCategory.HumidityMinPct;
                    max = zoneCategory.HumidityMaxPct;
                    break;

                case SensorType.temperature:
                    min = zoneCategory.TemperatureMinC;
                    max = zoneCategory.TemperatureMaxC;
                    break;

                case SensorType.light:
                    min = zoneCategory.LuminosityMinLux;
                    max = zoneCategory.LuminosityMaxLux;
                    break;

                default:
                    throw new ArgumentException($"Unknown record type: {zoneRecord.Type}");
            }

            zoneRecord.InRange = zoneRecord.Record >= min && zoneRecord.Record <= max;

            if (!zoneRecord.InRange)
            {
                var reason = zoneRecord.Record > max
                    ? AlertReason.Over
                    : AlertReason.Under;

                var alert = new ZoneSensorAlertPartial
                {
                    SensorId = zoneRecord.SensorId,
                    SensorType = zoneRecord.Type,
                    Reason = reason
                };

                await _zoneAlertService.CreateAsync(alert);
            }
        }
    }
}