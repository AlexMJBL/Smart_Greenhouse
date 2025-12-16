using Greenhouse_API.DTOs;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class ZoneSensorAlertService : IZoneSensorAlertService
    {
                private IRepository<ZoneSensorAlert> _repository;
        private readonly ILogger<ZoneSensorAlertService> _logger;
        private readonly ISensorService _sensorService;
        public ZoneSensorAlertService(IRepository<ZoneSensorAlert> repository, ILogger<ZoneSensorAlertService> logger, ISensorService sensorService)
        {
            _repository = repository;
            _logger = logger;
            _sensorService = sensorService;
        }

        public async Task<IEnumerable<ZoneSensorAlertDto>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all sensor alerts");
            var alerts = await _repository.GetAllAsync();
            return alerts.Select(zoneSensorAlert => new ZoneSensorAlertDto
            {
                Id = zoneSensorAlert.Id,
                Reason = zoneSensorAlert.Reason,
                SensorId = zoneSensorAlert.SensorId,
                CreatedAt = zoneSensorAlert.CreatedAt,
                SensorType = zoneSensorAlert.SensorType 
            });
        }

        public async Task<ZoneSensorAlertDto?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving sensor alert with ID {AlertId}", id);
            var alert = await _repository.GetByIdAsync(id);
            if (alert == null)
            {
                _logger.LogWarning("Sensor alert with ID {AlertId} not found", id);
                return null;
            }

            return new ZoneSensorAlertDto
            {
                Id = alert.Id,
                Reason = alert.Reason,
                SensorId = alert.SensorId,
                CreatedAt = alert.CreatedAt
            };
        }

        public async Task<ZoneSensorAlertDto> CreateAsync(ZoneSensorAlertPartial partial)
        {
            var sensor = _sensorService.GetByIdAsync(partial.SensorId);
            if(sensor == null)
            {
                _logger.LogWarning("Sensor with ID: {SensorId} not found for alert creation", partial.SensorId);
                throw new KeyNotFoundException("Sensor not found");
            }

            var alert = new ZoneSensorAlert
            {
                SensorId = partial.SensorId,
                Reason = partial.Reason,
                SensorType = partial.SensorType,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(alert);
            _logger.LogInformation("Sensor alert created with ID: {AlertId}", alert.Id);

            return new ZoneSensorAlertDto
            {
                Id = alert.Id,
                SensorId = alert.SensorId,
                Reason = alert.Reason,
                SensorType = alert.SensorType,
                CreatedAt = alert.CreatedAt
            };
        }

        public async Task DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);  
            if(!deleted)
            {
                _logger.LogWarning("Sensor alert with ID {AlertId} not found for deletion", id);
                throw new KeyNotFoundException("Sensor alert not found");
            }  

            _logger.LogInformation("Sensor alert with ID {AlertId} deleted", id);
        }
    }
}
