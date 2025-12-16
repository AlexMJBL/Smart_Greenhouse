using Greenhouse_API.DTOs;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class PlantSensorAlertService : IPlantSensorAlertService
    {
        private readonly IRepository<PlantSensorAlert> _repository;
        private readonly ILogger<PlantSensorAlertService> _logger;
        private readonly IPlantService _plantService;
        
        public PlantSensorAlertService(IRepository<PlantSensorAlert> repository, ILogger<PlantSensorAlertService> logger, IPlantService plantService)
        {
            _repository = repository;
            _logger = logger;
            _plantService = plantService;
        }

        public async Task<PlantSensorAlertDto> CreateAsync(PlantSensorAlertPartial alert)
        {
            var plante = await _plantService.GetByIdAsync(alert.PlantId);
            if (plante == null)
            {
                _logger.LogWarning("Plant with ID: {PlantId} not found for sensor alert creation", alert.PlantId);
                throw new KeyNotFoundException("Plant not found");
            }

            var plantSensorAlert = new PlantSensorAlert
            {
                PlantId = alert.PlantId,
                Reason = alert.Reason,
                SensorType = alert.SensorType,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(plantSensorAlert);
            _logger.LogInformation("PlantSensorAlert created with ID: {AlertId}", plantSensorAlert.Id);

            return new PlantSensorAlertDto
            {
                Id = plantSensorAlert.Id,
                PlantId = plantSensorAlert.PlantId,
                Reason = plantSensorAlert.Reason,
                SensorType = plantSensorAlert.SensorType,
                CreatedAt = plantSensorAlert.CreatedAt
            };
        }

        public async Task DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if(!deleted)
            {
                _logger.LogWarning("PlantSensorAlert with ID {AlertId} not found for deletion", id);
                throw new KeyNotFoundException("PlantSensorAlert not found");
            }

            _logger.LogInformation("PlantSensorAlert with ID {AlertId} deleted", id);
        }

        public async Task<IEnumerable<PlantSensorAlertDto>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all PlantSensorAlerts");
            var alerts = await _repository.GetAllAsync();

            return alerts.Select(alert => new PlantSensorAlertDto
            {
                Id = alert.Id,
                PlantId = alert.PlantId,
                Reason = alert.Reason,
                SensorType = alert.SensorType,
                CreatedAt = alert.CreatedAt
            });
        }

        public async Task<PlantSensorAlertDto?> GetByIdAsync(int id)
        {
            var alert = await _repository.GetByIdAsync(id);
            if(alert == null)
            {
                _logger.LogWarning("PlantSensorAlert with ID {AlertId} not found", id);
                return null;
            }

            _logger.LogInformation("PlantSensorAlert with ID {AlertId} retrieved", id);
            return new PlantSensorAlertDto
            {
                Id = alert.Id,
                PlantId = alert.PlantId,
                Reason = alert.Reason,
                SensorType = alert.SensorType,
                CreatedAt = alert.CreatedAt
            };
        }
    }
}
