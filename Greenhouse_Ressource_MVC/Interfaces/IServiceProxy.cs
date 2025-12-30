namespace Greenhouse_Ressource_MVC.Interfaces
{
    public interface IServiceProxy<T,TWrite>
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> CreateAsync(TWrite entity);
        Task<T?> UpdateAsync(int id, TWrite entity);
        Task DeleteAsync(int id);
    }
}
