namespace Greenhouse_Data_MVC.Interfaces
{
    public interface IServiceProxy<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
