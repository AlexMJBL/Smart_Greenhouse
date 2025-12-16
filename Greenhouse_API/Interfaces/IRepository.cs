// This generic repository interface defines a standardized set of data access 
// operations for any entity type. It abstracts CRUD functionality and supports 
// optional filtering through expression trees, allowing services to interact with 
// data sources in a consistent and flexible way. Implementations of this interface 
// ensure separation of concerns by isolating persistence logic from business logic.

using Greenhouse_API.Models;
using System.Linq.Expressions;

namespace Greenhouse_API.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T t);
        Task<T> UpdateAsync(int id, T u);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllWithFilter(Expression<Func<T, bool>>? filter = null);
        Task SaveAsync();
    }
}
