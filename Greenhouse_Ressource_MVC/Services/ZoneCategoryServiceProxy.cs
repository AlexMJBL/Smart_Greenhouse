using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Interfaces;

namespace Greenhouse_Ressource_MVC.Services
{
    public class ZoneCategoryServiceProxy : ServiceProxy<ZoneCategoryDto, ZoneCategoryWriteDto> , IZoneCategoryServiceProxy
    {
        public ZoneCategoryServiceProxy(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory, "api/zoneCategories")
        {
        }
    }
}
