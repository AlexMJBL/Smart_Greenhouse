namespace Greenhouse_API.Interfaces
{
    public interface ICrdService<TDto, TWriteDto>
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(int id);
        Task<TDto> CreateAsync(TWriteDto dto);
        Task DeleteAsync(int id);
    }
}
