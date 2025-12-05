using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class ZoneCategoryService : IRepository<ZoneCategory, int>
    {
        private SerreContext _context;
        private readonly ILogger<ZoneCategoryService> _logger;
        public ZoneCategoryService(SerreContext context, ILogger<ZoneCategoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<ZoneCategory>> GetAllAsync()
        {
            return await _context.ZoneCategories.ToListAsync();
        }

        public async Task<IEnumerable<ZoneCategory>> GetAllWithFilter(Expression<Func<ZoneCategory, bool>>? filter = null)
        {
            IQueryable<ZoneCategory> query = _context.ZoneCategories;
            if (filter != null)
                query = query.Where(filter);
                
            return await query.ToListAsync();
        }

        public async Task<ZoneCategory?> GetByIdAsync(int id)
        {
            return await _context.ZoneCategories.FindAsync(id);
        }

        public async Task<ZoneCategory> AddAsync(ZoneCategory zoneCategory)
        {
            _context.ZoneCategories.Add(zoneCategory);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"ZoneCategory with ID {zoneCategory.Id} added successfully.");
            return zoneCategory;
        }

        public async Task<ZoneCategory> UpdateAsync(int id, ZoneCategory zoneCategory)
        {
            if (id != zoneCategory.Id)
            {
                throw new ArgumentException("ID mismatch between route and body.");
            }

            _context.ZoneCategories.Update(zoneCategory);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"ZoneCategory with ID {zoneCategory.Id} updated successfully.");
            return zoneCategory;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var zoneCategory = await _context.ZoneCategories.FindAsync(id);
            if (zoneCategory == null)
                return false;
            
            _context.ZoneCategories.Remove(zoneCategory);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"ZoneCategory with ID {id} deleted successfully.");
            return true;
        }
    }
}
