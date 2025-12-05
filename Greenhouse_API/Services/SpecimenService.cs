using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;

namespace Greenhouse_API.Services
{
    public class SpecimenService : IRepository<Specimen>
    {
        private SerreContext _context;
        private readonly ILogger<SpecimenService> _logger;
        public Task<Specimen> AddAsync(Specimen t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Specimen>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Specimen>> GetAllWithFilter(Func<Task, bool>? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<Specimen> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Specimen> UpdateAsync(int id, Specimen u)
        {
            throw new NotImplementedException();
        }
    }
}
