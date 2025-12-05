using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class WateringService : IRepository<Watering, int>
    {
        private SerreContext _context;
        private readonly ILogger<WateringService> _logger;

        private readonly IRepository<Plant, int> _plantService;

        public WateringService(SerreContext context, ILogger<WateringService> logger, IRepository<Plant, int> plantService)
        {
            _context = context;
            _logger = logger;
            _plantService = plantService;   
        }


        public async Task<IEnumerable<Watering>> GetAllAsync()
        {
           return await _context.Waterings.ToListAsync();
        }

        public async Task<IEnumerable<Watering>> GetAllWithFilter(Expression<Func<Watering, bool>>? filter = null)
        {
            IQueryable<Watering> query = _context.Waterings;

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<Watering?> GetByIdAsync(int id)
        {
            return await _context.Waterings.FindAsync(id);
        }
        
        public async Task<Watering> AddAsync(Watering watering)
        {
            var plant = await _plantService.GetByIdAsync(watering.PlantId);
            if (plant == null)
            {
                throw new ArgumentException($"Plant with ID {watering.PlantId} does not exist.");
            }

            _context.Waterings.Add(watering);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Added new Watering with ID {Id}", watering.Id);
            return watering;
        }
        public async Task<Watering> UpdateAsync(int id, Watering watering)
        {
            var plant = await _plantService.GetByIdAsync(watering.PlantId);
            if (plant == null)
            {
                throw new ArgumentException($"Plant with ID {watering.PlantId} does not exist.");
            }
            
            if (id != watering.Id)
            {
                _logger.LogError("Watering ID mismatch: {Id} != {WateringId}", id, watering.Id);
                throw new ArgumentException("Watering ID mismatch.");
            }

            _context.Waterings.Update(watering);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated Watering with ID {Id}", watering.Id);
            return watering;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var watering = await GetByIdAsync(id);
            if (watering == null)
                return false;
            
            _context.Waterings.Remove(watering);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted Watering with ID {Id}", id);
            return true;
        }
    }
}
