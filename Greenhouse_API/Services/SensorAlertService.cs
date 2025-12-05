using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class SensorAlertService : IRepository<SensorAlert, int>
    {
        private SerreContext _context;
        private readonly ILogger<SensorAlertService> _logger;
        private readonly IRepository<Sensor, string> _sensorService;
        public SensorAlertService(SerreContext context, ILogger<SensorAlertService> logger, IRepository<Sensor, string> sensorService)
        {
            _context = context;
            _logger = logger;
            _sensorService = sensorService;
        }


        public async Task<IEnumerable<SensorAlert>> GetAllAsync()
        {
           return await _context.SensorAlerts.ToListAsync();
        }

        public async Task<IEnumerable<SensorAlert>> GetAllWithFilter(Expression<Func<SensorAlert, bool>>? filter = null)
        {
            IQueryable<SensorAlert> query = _context.SensorAlerts;

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<SensorAlert?> GetByIdAsync(int id)
        {
            return await _context.SensorAlerts.FindAsync(id);
        }

        
        public async Task<SensorAlert> AddAsync(SensorAlert sensorAlert)
        {
            var sensor = await _sensorService.GetByIdAsync(sensorAlert.SensorId);
            if (sensor == null)
            {
                throw new ArgumentException($"Sensor with ID {sensorAlert.SensorId} does not exist.");
            }

            _context.SensorAlerts.Add(sensorAlert);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Added new SensorAlert with ID {Id}", sensorAlert.Id);
            return sensorAlert;
        }
        public async Task<SensorAlert> UpdateAsync(int id, SensorAlert sensorAlert)
        {
            var sensor = await _sensorService.GetByIdAsync(sensorAlert.SensorId);
            if (sensor == null)
            {
                throw new ArgumentException($"Sensor with ID {sensorAlert.SensorId} does not exist.");
            }

            if( id != sensorAlert.Id)
            {
                throw new ArgumentException("SensorAlert ID mismatch.");
            }

            _context.SensorAlerts.Update(sensorAlert);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated SensorAlert with ID {Id}", sensorAlert.Id);
            return sensorAlert;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sensorAlert = await GetByIdAsync(id);
            if (sensorAlert == null)
                return false;
            
            _context.SensorAlerts.Remove(sensorAlert);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted SensorAlert with ID {Id}", id);
            return true;
        }
    }
}
