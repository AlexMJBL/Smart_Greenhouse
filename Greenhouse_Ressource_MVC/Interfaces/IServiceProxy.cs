namespace Greenhouse_Ressource_MVC.Interfaces
{
    public interface IServiceProxy<T,U>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(U entity);
        Task<T> UpdateAsync(int id, U entity);
        Task DeleteAsync(int id);
    }
}
