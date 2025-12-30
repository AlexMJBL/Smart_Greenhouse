using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;

namespace Greenhouse_Data_MVC.Services
{
    public class ObservationServiceProxy : ServiceProxy<ObservationDto> , IObservationServiceProxy
    {
        public ObservationServiceProxy(IHttpClientFactory httpClientFactory)
        : base (httpClientFactory, "api/observations")
        { 
        }
    }
}
