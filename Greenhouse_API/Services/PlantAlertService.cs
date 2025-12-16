using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class PlantAlertService : IRepository<PlantSensorAlert, int>
    {
        private SerreContext _context;
        private readonly ILogger<PlantAlertService> _logger;
        private readonly IRepository<Plant, int> _plantService;
        
        public PlantAlertService(SerreContext context, ILogger<PlantAlertService> logger, IRepository<Plant, int> plantService)
        {
            _context = context;
            _logger = logger;
            _plantService = plantService;
        }


        public async Task<IEnumerable<PlantSensorAlert>> GetAllAsync()
        {
           return await _context.PlantAlerts.ToListAsync();
        }

        public async Task<IEnumerable<PlantSensorAlert>> GetAllWithFilter(Expression<Func<PlantSensorAlert, bool>>? filter = null)
        {
            IQueryable<PlantSensorAlert> query = _context.PlantAlerts;

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<PlantSensorAlert?> GetByIdAsync(int id)
        {
            return await _context.PlantAlerts.FindAsync(id);
        }

        
        public async Task<PlantSensorAlert> AddAsync(PlantSensorAlert plantAlert)
        {
            var plant = await _plantService.GetByIdAsync(plantAlert.PlantId);
            if (plant == null)
            {
                throw new ArgumentException($"Plant with ID {plantAlert.PlantId} does not exist.");
            }

            _context.PlantAlerts.Add(plantAlert);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Added new PlantAlert with ID {Id}", plantAlert.Id);
            return plantAlert;
        }
        public async Task<PlantSensorAlert> UpdateAsync(int id, PlantSensorAlert plantAlert)
        {
            var plant = await _plantService.GetByIdAsync(plantAlert.PlantId);
            if (plant == null)
            {
                throw new ArgumentException($"Plant with ID {plantAlert.PlantId} does not exist.");
            }

            if (id != plantAlert.Id)
            {
                _logger.LogError("PlantAlert ID mismatch: {Id} != {PlantAlertId}", id, plantAlert.Id);
                throw new ArgumentException("PlantAlert ID mismatch.");
            }

            _context.PlantAlerts.Update(plantAlert);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated PlantAlert with ID {Id}", plantAlert.Id);
            return plantAlert;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var plantAlert = await GetByIdAsync(id);
            if (plantAlert == null)
                return false;
            
            _context.PlantAlerts.Remove(plantAlert);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted PlantAlert   with ID {Id}", id);
            return true;
        }
    }
}
