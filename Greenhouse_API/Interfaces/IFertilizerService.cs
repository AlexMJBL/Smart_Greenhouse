using Greenhouse_API.DTOs;
using Greenhouse_API.Models;

namespace Greenhouse_API.Interfaces
{
    public interface IFertilizerService : ICrudService<FertilizerDto, FertilizerWriteDto>
    {
    }
}
