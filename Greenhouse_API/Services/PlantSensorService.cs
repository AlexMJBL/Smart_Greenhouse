using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;

namespace Greenhouse_API.Services
{
    public class PlantSensorService : IPlantSensorService
    {
        private readonly IRepository<PlantSensor> _repository;
        private readonly ILogger<PlantSensorService> _logger;
        private readonly IPlantService _plantService;

        public PlantSensorService(
            IRepository<PlantSensor> repository,
            ILogger<PlantSensorService> logger,
            IPlantService plantService)
        {
            _repository = repository;
            _logger = logger;
            _plantService = plantService;
        }

        public async Task<IEnumerable<PlantSensorDto>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all plant sensors");

            var sensors = await _repository.GetAllAsync();

            return sensors.Select(sensor => new PlantSensorDto
            {
                Id = sensor.Id,
                Name = sensor.Name,
                Type = sensor.Type,
                PlantId = sensor.PlantId,
            });
        }

        public async Task<PlantSensorDto?> GetByIdAsync(int id)
        {
            var sensor = await _repository.GetByIdAsync(id);

            if (sensor == null)
            {
                _logger.LogWarning("Plant sensor with ID: {SensorId} not found", id);
                return null;
            }

            return new PlantSensorDto
            {
                Id = sensor.Id,
                Name = sensor.Name,
                Type = sensor.Type,
                PlantId = sensor.PlantId,
            };
        }

        public async Task<PlantSensorDto> CreateAsync(PlantSensorWriteDto dto)
        {
            var plant = await _plantService.GetByIdAsync(dto.PlantId);

            if (plant == null)
            {
                _logger.LogWarning("Plant with ID: {PlantId} not found for sensor creation", dto.PlantId);
                throw new NotFoundException("Plant not found");
            }

            var sensor = new PlantSensor
            {
                Name = dto.Name,
                Type = dto.Type,
                PlantId = dto.PlantId
            };

            await _repository.AddAsync(sensor);

            _logger.LogInformation("Plant sensor created with ID: {SensorId}", sensor.Id);

            return new PlantSensorDto
            {
                Id = sensor.Id,
                Name = sensor.Name,
                Type = sensor.Type,
                PlantId = sensor.PlantId
            };
        }

        public async Task<PlantSensorDto> UpdateAsync(int id, PlantSensorWriteDto dto)
        {
            var sensor = await _repository.GetByIdAsync(id);

            if (sensor == null)
            {
                _logger.LogWarning("Plant sensor with ID: {SensorId} not found for update", id);
                throw new NotFoundException("Plant sensor not found");
            }

            var plant = await _plantService.GetByIdAsync(dto.PlantId);

            if (plant == null)
            {
                _logger.LogWarning("Plant with ID {PlantId} not found for sensor update", dto.PlantId);
                throw new NotFoundException("Plant not found");
            }

            sensor.Name = dto.Name;
            sensor.Type = dto.Type;
            sensor.PlantId = dto.PlantId;

            await _repository.SaveAsync();

            _logger.LogInformation("Plant sensor with ID {SensorId} updated", id);

            return new PlantSensorDto
            {
                Id = sensor.Id,
                Name = sensor.Name,
                Type = sensor.Type,
                PlantId = sensor.PlantId,
            };
        }

        public async Task DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
            {
                _logger.LogWarning("Plant sensor with ID {SensorId} not found for deletion", id);
                throw new NotFoundException("Plant sensor not found");
            }

            _logger.LogInformation("Plant sensor with ID {SensorId} deleted", id);
        }
    }
}