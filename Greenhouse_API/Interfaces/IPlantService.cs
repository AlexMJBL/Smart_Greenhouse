using Greenhouse_API.DTOs;

namespace Greenhouse_API.Interfaces
{
    public interface IPlantService : ICrudService<PlantDto, PlantWriteDto>
    {
    }
}
