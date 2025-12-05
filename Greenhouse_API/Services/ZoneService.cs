using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;

namespace Greenhouse_API.Services
{
    public class ZoneService : IRepository<Zone>
    {
        private SerreContext _context;
        private readonly ILogger<ZoneService> _logger;
        public ZoneService(SerreContext context, ILogger<ZoneService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<Zone> AddAsync(Zone t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Zone>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Zone>> GetAllWithFilter(Func<Task, bool>? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<Zone> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Zone> UpdateAsync(int id, Zone u)
        {
            throw new NotImplementedException();
        }
    }
}
