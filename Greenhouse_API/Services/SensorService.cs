using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class SensorService : IRepository<Sensor, string>
    {
        private SerreContext _context;
        private readonly ILogger<SensorService> _logger;
        private readonly IRepository<Zone, int> _zoneService;

        public SensorService(SerreContext context, ILogger<SensorService> logger, IRepository<Zone, int> zoneService)
        {
            _context = context;
            _logger = logger;
            _zoneService = zoneService;
        }


        public async Task<IEnumerable<Sensor>> GetAllAsync()
        {
            return await _context.Sensors.ToListAsync();
        }

        public async Task<IEnumerable<Sensor>> GetAllWithFilter(Expression<Func<Sensor, bool>>? filter = null)
        {
            IQueryable<Sensor> query = _context.Sensors;

            if (filter != null)
                query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<Sensor?> GetByIdAsync(string id)
        {
            return await _context.Sensors.FindAsync(id);
        }

        public async Task<Sensor> AddAsync(Sensor sensor)
        {
           var zone = await _zoneService.GetByIdAsync(sensor.ZoneId);
            if (zone == null)
              {
                throw new ArgumentException($"Zone with ID ${sensor.ZoneId} does not exist.");
            }

            _context.Sensors.Add(sensor);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Sensor with ID {sensor.Id} added successfully.");
            return sensor;
        }
        public async Task<Sensor> UpdateAsync(string id, Sensor sensor)
        {
            var zone = await _zoneService.GetByIdAsync(sensor.ZoneId);

            if (zone == null)
            {
                throw new ArgumentException($"Zone with ID ${sensor.ZoneId} does not exist.");
            }

            if(id != sensor.Id)
            {
                throw new ArgumentException("Sensor ID mismatch.");
            }

            _context.Sensors.Update(sensor);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Sensor with ID {sensor.Id} updated successfully.");
            return sensor;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var plant = await GetByIdAsync(id);

            if (plant == null)
                return false;
            
            _context.Sensors.Remove(plant);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Sensor with ID {id} deleted successfully.");
            return true;
        }
    }
}
