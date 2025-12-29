using Greenhouse_Ressource_MVC.Dtos;

namespace Greenhouse_Ressource_MVC.Services
{
    public class PlantServiceProxy : ServiceProxy<PlantDto, PlantWriteDto>
    {
        public PlantServiceProxy(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory, "api/plants")
        {
        }
    }
}
