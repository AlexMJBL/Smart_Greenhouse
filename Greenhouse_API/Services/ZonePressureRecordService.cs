using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Greenhouse_API.Services;
using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;

namespace Greenhouse_API.Services
{
    public class ZonePressureRecordService : IZonePressureRecordService
    {
        private IRepository<ZonePressureRecord> _repository;
        private readonly ILogger<ZonePressureRecordService> _logger;
        private readonly IZoneService _zoneService;
        private readonly ISensorService _sensorService;
        public ZonePressureRecordService(IRepository<ZonePressureRecord> repository,IZoneService zoneService ,
            ISensorService sensorService, ILogger<ZonePressureRecordService> logger)
        {
            _repository = repository;
            _logger = logger;
            _zoneService = zoneService;
            _sensorService = sensorService;
        }

        public async Task<IEnumerable<ZonePressureRecordDto>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all zone pressure records");
            var records = await _repository.GetAllAsync();
            return records.Select(r => new ZonePressureRecordDto
            {
                Id = r.Id,
                ZoneId = r.ZoneId,
                RecordedHPa = r.RecordedHPa,
                SensorId = r.SensorId,
                CreatedAt = r.CreatedAt
            });
        }

        public async Task<ZonePressureRecordDto?> GetByIdAsync(int id)
        {
            var record = await _repository.GetByIdAsync(id);
            if (record == null)
            {
                _logger.LogWarning("Zone pressure record with ID: {ZonePressureRecordId} not found", id);
                return null;
            }

            _logger.LogInformation("Zone pressure record with ID: {ZonePressureRecordId} retrieved", id);
            return new ZonePressureRecordDto
            {
                Id = record.Id,
                ZoneId = record.ZoneId,
                RecordedHPa = record.RecordedHPa,
                SensorId = record.SensorId,
                CreatedAt = record.CreatedAt
            };
        }

        public async Task<ZonePressureRecordDto> CreateAsync(ZonePressureRecordWriteDto dto)
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
                _logger.LogWarning("Zone with ID: {ZoneId} not found for pressure record creation", dto.ZoneId);
                throw new NotFoundException("Zone not found");
            }

            var record = new ZonePressureRecord
            {
                RecordedHPa = dto.RecordedHPa,
                ZoneId = sensor.ZoneId,
                SensorId = dto.SensorId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(record);
            _logger.LogInformation("Zone pressure record created with ID: {ZonePressureRecordId}", record.Id);

            return new ZonePressureRecordDto
            {
                Id = record.Id,
                RecordedHPa = record.RecordedHPa,
                ZoneId = record.ZoneId,
                SensorId = record.SensorId,
                CreatedAt = record.CreatedAt
            };
        }

        public async Task<ZonePressureRecordDto> UpdateAsync(int id, ZonePressureRecordWriteDto dto)
        {
            var record = _repository.GetByIdAsync(id).Result;
            if (record == null)
            {
                _logger.LogWarning("Zone pressure record with ID: {ZonePressureRecordId} not found for update", id);
                throw new NotFoundException("Zone pressure record not found");
            }

            var zone = _repository.GetByIdAsync(dto.ZoneId).Result;
            if (zone == null)
            {
                _logger.LogWarning("Zone with ID: {ZoneId} not found for pressure record update", dto.ZoneId);
                throw new NotFoundException("Zone not found");
            }

            var sensor = await _sensorService.GetByIdAsync(dto.SensorId);
            if (sensor == null)
            {
                _logger.LogWarning("Sensor with ID: {SensorId} not found for pressure record creation", dto.SensorId);
                throw new NotFoundException("Zone not found");
            }

            record.RecordedHPa = dto.RecordedHPa;
            record.ZoneId = dto.ZoneId;
            record.SensorId = dto.SensorId;

            await _repository.SaveAsync();
            _logger.LogInformation("Zone pressure record with ID: {ZonePressureRecordId} updated", id);

            return new ZonePressureRecordDto
            {
                Id = record.Id,
                RecordedHPa = record.RecordedHPa,
                ZoneId = record.ZoneId,
                SensorId = record.SensorId,
                CreatedAt = record.CreatedAt
            };
        }

        public async Task DeleteAsync(int id)
        {
            var deleted =  await _repository.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogWarning("Zone pressure record with ID {ZonePressureRecordId} not found for deletion", id);
                throw new NotFoundException("Zone pressure record not found");
            }
            
            _logger.LogInformation("Zone pressure record with ID {ZonePressureRecordId} deleted", id);
        }
    }
}
