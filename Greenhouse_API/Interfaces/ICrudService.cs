namespace Greenhouse_API.Interfaces
{
    public interface ICrudService<TDto, TWriteDto>
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(int id);
        Task<TDto> CreateAsync(TWriteDto dto);
        Task<TDto> UpdateAsync(int id, TWriteDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
