using Greenhouse_API.Models;

namespace Greenhouse_API.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T t);
        Task<T> UpdateAsync(int id, T u);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllWithFilter(Func<Task, bool>? filter = null);
    }
}
