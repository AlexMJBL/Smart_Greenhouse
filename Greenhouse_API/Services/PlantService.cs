using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class PlantService : IRepository<Plant, int>
    {
        private SerreContext _context;
        private readonly ILogger<PlantService> _logger;
        private readonly IRepository<SpecimenService, int> _specimenService;
        private readonly IRepository<ZoneService, int> _zoneService;

        public PlantService(SerreContext context, ILogger<PlantService> logger,
            IRepository<SpecimenService,int> repository,IRepository<ZoneService,int> zoneService)
        {
            _context = context;
            _logger = logger;
            _zoneService = zoneService;
            _specimenService = repository;
        }


        public async Task<IEnumerable<Plant>> GetAllAsync()
        {
           return await _context.Plants.ToListAsync();
        }

        public async Task<IEnumerable<Plant>> GetAllWithFilter(Expression<Func<Plant, bool>>? filter = null)
        {
            IQueryable<Plant> query = _context.Plants;

            if (filter != null)
                query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<Plant?> GetByIdAsync(int id)
        {
            return await _context.Plants.FindAsync(id);
        }

        
        public async Task<Plant> AddAsync(Plant plant)
        {
            //Specimen must exist
            var specimen = await _specimenService.GetByIdAsync(plant.SpecimenId);
            if(specimen == null)
            {
                throw new ArgumentException($"Specimen with ID {plant.SpecimenId} does not exist.");
            }

            //Zone must exist
            var zone = await _zoneService.GetByIdAsync(plant.ZoneId);
            if(zone == null)
            {
                throw new ArgumentException($"Zone with ID {plant.ZoneId} does not exist.");
            }

            //Mom Plant is optional but if provided, it must exist
            if (plant.MomId.HasValue)
            {
                var momPlant = await GetByIdAsync(plant.MomId.Value);
                if(momPlant == null)
                {
                    throw new ArgumentException($"Mom Plant with ID {plant.MomId.Value} does not exist.");
                }
            }

            plant.CreatedAt = DateTime.Now;
            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Added new Plant with ID {Id}", plant.Id);
            return plant;
        }
        public async Task<Plant> UpdateAsync(int id, Plant plant)
        {
            //Specimen must exist
            var specimen = await _specimenService.GetByIdAsync(plant.SpecimenId);
            if (specimen == null)
            {
                throw new ArgumentException($"Specimen with ID {plant.SpecimenId} does not exist.");
            }

            //Zone must exist
            var zone = await _zoneService.GetByIdAsync(plant.ZoneId);
            if (zone == null)
            {
                throw new ArgumentException($"Zone with ID {plant.ZoneId} does not exist.");
            }

            //Mom Plant is optional but if provided, it must exist
            if (plant.MomId.HasValue)
            {
                var momPlant = await GetByIdAsync(plant.MomId.Value);
                if (momPlant == null)
                {
                    throw new ArgumentException($"Mom Plant with ID {plant.MomId.Value} does not exist.");
                }
            }

            if (id != plant.Id)
            {
                _logger.LogError("Plant ID mismatch: {Id} != {PlantId}", id, plant.Id);
                throw new ArgumentException("Plant ID mismatch.");
            }
            _context.Plants.Update(plant);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated Plant with ID {Id}", plant.Id);

            return plant;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var plant = await GetByIdAsync(id);
            if (plant == null)
                return false;
            
            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted Plant with ID {Id}", id);
            return true;
        }

       
    }
}
