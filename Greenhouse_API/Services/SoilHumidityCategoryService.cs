using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class SoilHumidityCategoryService : IRepository<SoilHumidityCategory, int>
    {
        private SerreContext _context;
        private readonly ILogger<SoilHumidityCategoryService> _logger;
        public SoilHumidityCategoryService(SerreContext context, ILogger<SoilHumidityCategoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<SoilHumidityCategory>> GetAllAsync()
        {
            return await _context.SoilHumidityCategories.ToListAsync();
        }

        public async Task<IEnumerable<SoilHumidityCategory>> GetAllWithFilter(Expression<Func<SoilHumidityCategory, bool>>? filter = null)
        {
            IQueryable<SoilHumidityCategory> query = _context.SoilHumidityCategories;

            if (filter != null)
                query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<SoilHumidityCategory?> GetByIdAsync(int id)
        {
            return await _context.SoilHumidityCategories.FindAsync(id);
        }

        public async Task<SoilHumidityCategory> AddAsync(SoilHumidityCategory soilHumidityCategory)
        {
            _context.SoilHumidityCategories.Add(soilHumidityCategory);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"SoilHumidityCategory with ID {soilHumidityCategory.Id} added successfully.");
            return soilHumidityCategory;
        }
       

        public async Task<SoilHumidityCategory> UpdateAsync(int id, SoilHumidityCategory soilHumidityCategory)
        {
            if(id != soilHumidityCategory.Id)
            {
                throw new ArgumentException("ID mismatch between route and body.");
            }
            _context.SoilHumidityCategories.Update(soilHumidityCategory);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"SoilHumidityCategory with ID {soilHumidityCategory.Id} updated successfully.");
            return soilHumidityCategory;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var soilHumidityCategory = await GetByIdAsync(id);
            if (soilHumidityCategory == null)
                return false;
            
            _context.SoilHumidityCategories.Remove(soilHumidityCategory);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"SoilHumidityCategory with ID {id} deleted successfully.");
            return true;
        }
        
    }
}
