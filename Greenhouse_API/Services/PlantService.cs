using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;

namespace Greenhouse_API.Services
{
    public class PlantService : IRepository<Plant>
    {
        private SerreContext _context;
        private readonly ILogger<PlantService> _logger;

        public PlantService(SerreContext context, ILogger<PlantService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public Task<IEnumerable<Plant>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Plant>> GetAllWithFilter(Func<Task, bool>? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<Plant> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Plant> AddAsync(Plant plantt)
        {
            throw new NotImplementedException();
        }
        public Task<Plant> UpdateAsync(int id, Plant plant)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
