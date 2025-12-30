using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Interfaces;

namespace Greenhouse_Ressource_MVC.Services
{
    public class ObservationServiceProxy : ServiceProxy<ObservationDto, ObservationWriteDto> , IObservationServiceProxy
    {
        public ObservationServiceProxy(IHttpClientFactory httpClientFactory, IConfiguration config)
        : base (httpClientFactory, config, "observations")
        { 
        }
    }
}
