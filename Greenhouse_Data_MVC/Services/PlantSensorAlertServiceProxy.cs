using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;

namespace Greenhouse_Data_MVC.Services
{
    public class PlantSensorAlertServiceProxy : ServiceProxy<PlantSensorAlertDto> , IPlantSensorAlertServiceProxy
    {
        public PlantSensorAlertServiceProxy(IHttpClientFactory httpClientFactory, IConfiguration config)
        : base (httpClientFactory, config, "plantSensorAlerts")
        { 
        }
    }
}
