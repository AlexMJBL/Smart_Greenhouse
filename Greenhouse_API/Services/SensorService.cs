using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;

namespace Greenhouse_API.Services
{
    public class SensorService : IRepository<Sensor>
    {
        private SerreContext _context;
        private readonly ILogger<SensorService> _logger;

        public SensorService(SerreContext context, ILogger<SensorService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public Task<IEnumerable<Sensor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Sensor>> GetAllWithFilter(Func<Sensor, bool>? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<Sensor?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Sensor> AddAsync(Sensor sensor)
        {
            throw new NotImplementedException();
        }
        public Task<Sensor> UpdateAsync(int id, Sensor sensor)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
