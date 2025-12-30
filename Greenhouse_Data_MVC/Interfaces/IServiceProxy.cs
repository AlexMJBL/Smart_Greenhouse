namespace Greenhouse_Ressource_MVC.Interfaces
{
    public interface IServiceProxy<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
