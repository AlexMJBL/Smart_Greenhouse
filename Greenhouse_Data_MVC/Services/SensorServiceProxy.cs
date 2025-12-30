using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;

namespace Greenhouse_Data_MVC.Services
{
    public class SensorServiceProxy : ServiceProxy<SensorDto> , ISensorServiceProxy
    {
        public SensorServiceProxy(IHttpClientFactory httpClientFactory)
        : base (httpClientFactory, "api/sensors")
        { 
        }
    }
}
