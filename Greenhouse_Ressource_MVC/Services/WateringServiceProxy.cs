using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Interfaces;

namespace Greenhouse_Ressource_MVC.Services
{
    public class WateringServiceProxy : ServiceProxy<WateringDto, WateringWriteDto>, IWateringServiceProxy
    {
        public WateringServiceProxy(IHttpClientFactory httpClientFactory)
        :base(httpClientFactory, "api/waterings")
        { }
    }
}
