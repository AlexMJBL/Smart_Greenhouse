using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Interfaces;

namespace Greenhouse_Ressource_MVC.Services
{
    public class PlantServiceProxy : ServiceProxy<PlantDto, PlantWriteDto>, IPlantServiceProxy
    {
        public PlantServiceProxy(IHttpClientFactory httpClientFactory, IConfiguration config)
        : base(httpClientFactory, config, "plants")
        {
        }
    }
}
