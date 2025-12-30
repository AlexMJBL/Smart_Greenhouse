using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Interfaces;

namespace Greenhouse_Ressource_MVC.Services
{
    public class ZoneServiceProxy : ServiceProxy<ZoneDto, ZoneWriteDto>, IZoneServiceProxy
    {
        public ZoneServiceProxy(IHttpClientFactory httpClientFactory, IConfiguration config)
        : base(httpClientFactory, config, "zones")
        {
        }
    }
}
