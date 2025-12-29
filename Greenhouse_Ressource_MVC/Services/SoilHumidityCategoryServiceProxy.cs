using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Interfaces;

namespace Greenhouse_Ressource_MVC.Services
{
    public class SoilHumidityCategoryServiceProxy : ServiceProxy<SoilHumidityCategoryDto, SoilHumidityCategoryWriteDto>, ISoilHumidityCategoryServiceProxy
    {
        public SoilHumidityCategoryServiceProxy(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory, "api/soilHumidityCategories")
        {
        }
    }
}
