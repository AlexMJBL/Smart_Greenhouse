using Greenhouse_API.Data;
using Greenhouse_API.DTOs;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class FertilizerService : IFertilizerService
    {
        private GreenHouseDbContext _context;
        private readonly ILogger<FertilizerService> _logger;
        private readonly IPlantService<Plant> _plantService;
        public FertilizerService(GreenHouseDbContext context, ILogger<FertilizerService> logger, IPlantService<Plant> plantService)
        {
            _context = context;
            _logger = logger;
            _plantService = plantService;
        }


        public async Task<IEnumerable<FertilizerDto>> GetAllAsync()
        {
           return await _context.Fertilizers.ToListAsync();
        }

        public async Task<IEnumerable<FertilizerDto>> GetAllWithFilter(Expression<Func<Fertilizer, bool>>? filter = null)
        {
            IQueryable<Fertilizer> query = _context.Fertilizers;

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<Fertilizer?> GetByIdAsync(int id)
        {
            return await _context.Fertilizers.FindAsync(id);
        }

        
        public async Task<Fertilizer> AddAsync(FertilizerWriteDto fertilizerDto)
        {
            var plant = await _plantService.GetByIdAsync(fertilizerDto.PlantId);
            if (plant == null)
            {
                throw new ArgumentException($"Plant with ID {fertilizerDto.PlantId} does not exist.");
            }

            Fertilizer fertilizer = new Fertilizer
            {
                Type = fertilizerDto.Type,
                PlantId = fertilizerDto.PlantId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Fertilizers.Add(fertilizer);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Added new Fertilizer with ID {Id}", fertilizer.Id);
            return fertilizer;
        }
        public async Task<Fertilizer> UpdateAsync(int id, Fertilizer fertilizer)
        {
            var plant = await _plantService.GetByIdAsync(fertilizer.PlantId);
            if (plant == null)
            {
                throw new ArgumentException($"Plant with ID {fertilizer.PlantId} does not exist.");
            }

            if (id != fertilizer.Id)
            {
                _logger.LogError("Fertilizer ID mismatch: {Id} != {FertilizerId}", id, fertilizer.Id);
                throw new ArgumentException("Fertilizer ID mismatch.");
            }

            _context.Fertilizers.Update(fertilizer);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated Fertilizer with ID {Id}", fertilizer.Id);
            return fertilizer;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var fertilizer = await GetByIdAsync(id);
            if (fertilizer == null)
                return false;
            
            _context.Fertilizers.Remove(fertilizer);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted Fertilizer with ID {Id}", id);
            return true;
        }
    }
}
