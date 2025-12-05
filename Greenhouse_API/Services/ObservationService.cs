using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class ObservationService : IRepository<Observation, int>
    {
        private SerreContext _context;
        private readonly ILogger<ObservationService> _logger;

        private readonly IRepository<Plant, int> _plantService;

        public ObservationService(SerreContext context, ILogger<ObservationService> logger, IRepository<Plant, int> plantService)
        {
            _context = context;
            _logger = logger;
            _plantService = plantService;   
        }


        public async Task<IEnumerable<Observation>> GetAllAsync()
        {
           return await _context.Observations.ToListAsync();
        }

        public async Task<IEnumerable<Observation>> GetAllWithFilter(Expression<Func<Observation, bool>>? filter = null)
        {
            IQueryable<Observation> query = _context.Observations;

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<Observation?> GetByIdAsync(int id)
        {
            return await _context.Observations.FindAsync(id);
        }
        
        public async Task<Observation> AddAsync(Observation observation)
        {
            var plant = await _plantService.GetByIdAsync(observation.PlantId);
            if (plant == null)
            {
                throw new ArgumentException($"Plant with ID {observation.PlantId} does not exist.");
            }

            _context.Observations.Add(observation);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Added new Observation with ID {Id}", observation.Id);
            return observation;
        }
        public async Task<Observation> UpdateAsync(int id, Observation observation)
        {
            var plant = await _plantService.GetByIdAsync(observation.PlantId);
            if (plant == null)
            {
                throw new ArgumentException($"Plant with ID {observation.PlantId} does not exist.");
            }
            
            if (id != observation.Id)
            {
                _logger.LogError("Observation ID mismatch: {Id} != {ObservationId}", id, observation.Id);
                throw new ArgumentException("Observation ID mismatch.");
            }

            _context.Observations.Update(observation);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated Observation with ID {Id}", observation.Id);
            return observation;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var observation = await GetByIdAsync(id);
            if (observation == null)
                return false;
            
            _context.Observations.Remove(observation);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted Observation with ID {Id}", id);
            return true;
        }
    }
}
