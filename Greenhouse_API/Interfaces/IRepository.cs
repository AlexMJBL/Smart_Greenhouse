using Greenhouse_API.Models;
using System.Linq.Expressions;

namespace Greenhouse_API.Interfaces
{
    public interface IRepository<T, TKey> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(TKey id);
        Task<T> AddAsync(T t);
        Task<T> UpdateAsync(TKey id, T u);
        Task<bool> DeleteAsync(TKey id);
        Task<IEnumerable<T>> GetAllWithFilter(Expression<Func<T, bool>>? filter = null);
    }
}
