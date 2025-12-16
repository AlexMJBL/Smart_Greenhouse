using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class ZoneAlertService : IRepository<ZoneSensorAlert, int>
    {
        private SerreContext _context;
        private readonly ILogger<ZoneAlertService> _logger;

        private readonly IRepository<Zone, int> _zoneService;

        public ZoneAlertService(SerreContext context, ILogger<ZoneAlertService> logger, IRepository<Zone, int> zoneService)
        {
            _context = context;
            _logger = logger;
            _zoneService = zoneService;   
        }

        public async Task<IEnumerable<ZoneSensorAlert>> GetAllAsync()
        {
           return await _context.ZoneAlerts.ToListAsync();
        }

        public async Task<IEnumerable<ZoneSensorAlert>> GetAllWithFilter(Expression<Func<ZoneSensorAlert, bool>>? filter = null)
        {
            IQueryable<ZoneSensorAlert> query = _context.ZoneAlerts;

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<ZoneSensorAlert?> GetByIdAsync(int id)
        {
            return await _context.ZoneAlerts.FindAsync(id);
        }
        
        public async Task<ZoneSensorAlert> AddAsync(ZoneSensorAlert zoneAlert)
        {
            var zone = await _zoneService.GetByIdAsync(zoneAlert.ZoneId);
            if (zone == null)
            {
                throw new ArgumentException($"Zone with ID {zoneAlert.ZoneId} does not exist.");
            }

            _context.ZoneAlerts.Add(zoneAlert);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Added new ZoneAlert with ID {Id}", zoneAlert.Id);
            return zoneAlert;
        }
       
        public async Task<ZoneSensorAlert> UpdateAsync(int id, ZoneSensorAlert zoneAlert)
        {
            var zone = await _zoneService.GetByIdAsync(zoneAlert.ZoneId);
            if (zone == null)
            {
                throw new ArgumentException($"Zone with ID {zoneAlert.ZoneId} does not exist.");
            }
            
            if (id != zoneAlert.Id)
            {
                _logger.LogError("ZoneAlert ID mismatch: {Id} != {ZoneAlertId}", id, zoneAlert.Id);
                throw new ArgumentException("ZoneAlert ID mismatch.");
            }

            _context.ZoneAlerts.Update(zoneAlert);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated ZoneAlert with ID {Id}", zoneAlert.Id);
            return zoneAlert;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var zoneAlert = await GetByIdAsync(id);
            if (zoneAlert == null)
                return false;
            
            _context.ZoneAlerts.Remove(zoneAlert);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted ZoneAlert with ID {Id}", id);
            return true;
        }
    }
}
