using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Interfaces;

namespace Greenhouse_Ressource_MVC.Services
{
    public class PlantSensorServiceProxy
        : ServiceProxy<PlantSensorDto, PlantSensorWriteDto>, IPlantSensorServiceProxy
    {
        public PlantSensorServiceProxy(
            IHttpClientFactory httpClientFactory,
            IConfiguration config)
            : base(httpClientFactory, config, "plantsensors")
        {
        }
    }
}