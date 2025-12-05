using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class SoilHumidityCategoryService : IRepository<SoilHumidityCategory>
    {
        private SerreContext _context;
        private readonly ILogger<SoilHumidityCategoryService> _logger;
        public SoilHumidityCategoryService(SerreContext context, ILogger<SoilHumidityCategoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<SoilHumidityCategory> AddAsync(SoilHumidityCategory t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SoilHumidityCategory>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SoilHumidityCategory>> GetAllWithFilter(Expression<Func<Task, bool>>? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<SoilHumidityCategory?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SoilHumidityCategory> UpdateAsync(int id, SoilHumidityCategory u)
        {
            throw new NotImplementedException();
        }
    }
}
