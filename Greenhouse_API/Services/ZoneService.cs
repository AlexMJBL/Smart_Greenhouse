using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class ZoneService : IRepository<Zone, int>
    {
        private SerreContext _context;
        private readonly ILogger<ZoneService> _logger;
        private readonly IRepository<ZoneCategory, int> _zoneCategoryService;

        public ZoneService(SerreContext context, ILogger<ZoneService> logger, IRepository<ZoneCategory, int> zoneCategoryService)
        {
            _context = context;
            _logger = logger;
            _zoneCategoryService = zoneCategoryService;
        }

        public async Task<IEnumerable<Zone>> GetAllAsync()
        {
            return await _context.Zones.ToListAsync();
        }

        public async Task<IEnumerable<Zone>> GetAllWithFilter(Expression<Func<Zone, bool>>? filter = null)
        {
            IQueryable<Zone> query = _context.Zones;

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<Zone?> GetByIdAsync(int id)
        {
            return await _context.Zones.FindAsync(id);
        }

        public async Task<Zone> AddAsync(Zone zone)
        {   
            var zoneCategory = await _zoneCategoryService.GetByIdAsync(zone.ZoneCategoryId);
            if(zoneCategory == null)
            {
                throw new ArgumentException($"ZoneCategory with ID {zone.ZoneCategoryId} does not exist.");
            }

            _context.Zones.Add(zone);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Zone with ID {zone.Id} added successfully.");
            return zone;
        }

        public async Task<Zone> UpdateAsync(int id, Zone zone)
        {
            var zoneCategory = await _zoneCategoryService.GetByIdAsync(zone.ZoneCategoryId);
            if (zoneCategory == null)
            {
                throw new ArgumentException($"ZoneCategory with ID {zone.ZoneCategoryId} does not exist.");
            }

            if (id != zone.Id)
            {
                throw new ArgumentException("ID mismatch between route and body.");
            }

            _context.Zones.Update(zone);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Zone with ID {zone.Id} updated successfully.");
            return zone;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var zone = await _context.Zones.FindAsync(id);
            if (zone == null)
            {
                return false;
            }

            _context.Zones.Remove(zone);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Zone with ID {id} deleted successfully.");
            return true;
        }
    }
}
