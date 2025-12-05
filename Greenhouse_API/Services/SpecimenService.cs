using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class SpecimenService : IRepository<Specimen, int>
    {
        private SerreContext _context;
        private readonly ILogger<SpecimenService> _logger;
        private readonly IRepository<SoilHumidityCategory, int> _soilHumidityCategoryService;
        
        public SpecimenService(SerreContext context, ILogger<SpecimenService> logger, IRepository<SoilHumidityCategory, int> soilHumidityCategoryService)
        {
            _context = context;
            _logger = logger;
            _soilHumidityCategoryService = soilHumidityCategoryService;
        }

        public async Task<IEnumerable<Specimen>> GetAllAsync()
        {
            return await _context.Specimens.ToListAsync();
        }

        public async Task<IEnumerable<Specimen>> GetAllWithFilter(Expression<Func<Specimen, bool>>? filter = null)
        {
            IQueryable<Specimen> query = _context.Specimens;

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<Specimen?> GetByIdAsync(int id)
        {
            return await _context.Specimens.FindAsync(id);
        }

        public async Task<Specimen> AddAsync(Specimen specimen)
        {
            var soilHumidityCategory = await _soilHumidityCategoryService.GetByIdAsync(specimen.SoilHumidityCatId);
            if (soilHumidityCategory == null)
            {
                throw new ArgumentException($"SoilHumidityCategory with ID {specimen.SoilHumidityCatId} does not exist.");
            }

            _context.Specimens.Add(specimen);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Specimen with ID {specimen.Id} added successfully.");
            return specimen;
        }
        public async Task<Specimen> UpdateAsync(int id, Specimen specimen)
        {
            

            if (id != specimen.Id)
            {
                throw new ArgumentException("ID mismatch between route and body.");
            }
            _context.Specimens.Update(specimen);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Specimen with ID {specimen.Id} updated successfully.");
            return specimen;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var specimen = await _context.Specimens.FindAsync(id);
            if (specimen == null)
            {
                return false;
            }
            _context.Specimens.Remove(specimen);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Specimen with ID {id} deleted successfully.");
            return true;
        }
    }
}
