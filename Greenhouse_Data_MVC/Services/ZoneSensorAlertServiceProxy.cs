using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;

namespace Greenhouse_Data_MVC.Services
{
    public class ZoneSensorAlertServiceProxy : ServiceProxy<ZoneSensorAlertDto> , IZoneSensorAlertServiceProxy
    {
        public ZoneSensorAlertServiceProxy(IHttpClientFactory httpClientFactory)
        : base (httpClientFactory, "api/zoneSensorAlerts")
        { 
        }
    }
}
