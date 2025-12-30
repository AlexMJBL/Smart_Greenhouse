using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;

namespace Greenhouse_Data_MVC.Services
{
    public class ZoneServiceProxy : ServiceProxy<ZoneDto> , IZoneServiceProxy
    {
        public ZoneServiceProxy(IHttpClientFactory httpClientFactory)
        : base (httpClientFactory, "api/zones")
        { 
        }
    }
}
