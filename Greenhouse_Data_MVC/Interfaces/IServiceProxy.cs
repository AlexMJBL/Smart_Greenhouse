namespace Greenhouse_Ressource_MVC.Interfaces
{
    public interface IServiceProxy<T,U>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
