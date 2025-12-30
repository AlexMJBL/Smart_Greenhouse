using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;

namespace Greenhouse_Data_MVC.Services
{
    public class PlantServiceProxy : ServiceProxy<PlantDto> , IPlantServiceProxy
    {
        public PlantServiceProxy(IHttpClientFactory httpClientFactory, IConfiguration config)
        : base (httpClientFactory, config, "plants")
        { 
        }
    }
}
