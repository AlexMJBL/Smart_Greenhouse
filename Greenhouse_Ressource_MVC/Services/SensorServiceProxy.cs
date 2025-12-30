using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Interfaces;

namespace Greenhouse_Ressource_MVC.Services
{
    public class SensorServiceProxy : ServiceProxy<SensorDto, SensorWriteDto>, ISensorServiceProxy
    {
        public SensorServiceProxy(IHttpClientFactory httpClientFactory, IConfiguration config)
            :base(httpClientFactory, config, "sensors")
        { }
    }
}
