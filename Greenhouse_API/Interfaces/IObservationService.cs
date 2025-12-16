using Greenhouse_API.DTOs;
using Greenhouse_API.Models;

namespace Greenhouse_API.Interfaces
{
    public interface IObservationService : ICrudService<ObservationDto, ObservationWriteDto>
    {
    }
}
