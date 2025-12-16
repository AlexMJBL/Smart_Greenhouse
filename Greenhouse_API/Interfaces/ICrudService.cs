namespace Greenhouse_API.Interfaces
{
    public interface ICrudService<TDto, TWriteDto> : ICrdService<TDto, TWriteDto>
    {
        Task<TDto> UpdateAsync(int id, TWriteDto dto);
    }
}
