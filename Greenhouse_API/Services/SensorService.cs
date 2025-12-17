using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class SensorService : ISensorService
    {
        private IRepository<Sensor> _repository;
        private readonly ILogger<SensorService> _logger;
        private readonly IZoneService _zoneService;

        public SensorService(IRepository<Sensor> repository, ILogger<SensorService> logger, IZoneService zoneService)
        {
            _repository = repository;
            _logger = logger;
            _zoneService = zoneService;
        }

        public async Task<IEnumerable<SensorDto>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all sensors");
            var sensors = await _repository.GetAllAsync();

            return sensors.Select(sensor => new SensorDto
            {
                Id = sensor.Id,
                Type = sensor.Type,
                ZoneId = sensor.ZoneId,
                CreatedAt = sensor.CreatedAt
            });
        }

        public async Task<SensorDto?> GetByIdAsync(int id)
        {
            var sensor = await _repository.GetByIdAsync(id);
            if (sensor == null)
            {
                _logger.LogWarning("Sensor with ID: {SensorId} not found", id);
                return null;
            }

            _logger.LogInformation("Sensor with ID: {SensorId} retrieved", id);
            return new SensorDto
            {
                Id = sensor.Id,
                Type = sensor.Type,
                ZoneId = sensor.ZoneId,
                CreatedAt = sensor.CreatedAt
            };
        }

        public async Task<SensorDto> CreateAsync(SensorWriteDto dto)
        {
            var zone = await _zoneService.GetByIdAsync(dto.ZoneId);
            if (zone == null)
            {
                _logger.LogWarning("Zone with ID: {ZoneId} not found for sensor creation", dto.ZoneId);
                throw new NotFoundException("Zone not found");
            }

            var sensor = new Sensor
            {
                Type = dto.Type,
                ZoneId = dto.ZoneId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(sensor);
            _logger.LogInformation("Sensor created with ID: {SensorId}", sensor.Id);

            return new SensorDto
            {
                Id = sensor.Id,
                Type = sensor.Type,
                ZoneId = sensor.ZoneId,
                CreatedAt = sensor.CreatedAt
            };
        }

        public async Task<SensorDto> UpdateAsync(int id, SensorWriteDto dto)
        {
            var sensor = await _repository.GetByIdAsync(id);
            if (sensor == null)
            {
                _logger.LogWarning("Sensor with ID: {SensorId} not found for update", id);
                throw new NotFoundException("Sensor not found");
            }

            var zone = await _zoneService.GetByIdAsync(dto.ZoneId);
            if (zone == null)
            {
                _logger.LogWarning("Zone with ID {ZoneId} not found for sensor update", dto.ZoneId);
                throw new NotFoundException("Zone not found");
            }

            sensor.Type = dto.Type;
            sensor.ZoneId = dto.ZoneId;
            await _repository.SaveAsync();

            _logger.LogInformation("Sensor with ID {SensorId} updated", id);
            return new SensorDto
            {
                Id = sensor.Id,
                Type = sensor.Type,
                ZoneId = sensor.ZoneId,
                CreatedAt = sensor.CreatedAt
            };
        }

        public async Task DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogWarning("Sensor with ID {SensorId} not found for deletion", id);
                throw new NotFoundException("Sensor not found");
            }
            _logger.LogInformation("Sensor with ID {SensorId} deleted", id);
        }
    }
}
