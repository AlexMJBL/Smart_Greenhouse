using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;

namespace Greenhouse_Data_MVC.Services
{
    public class WateringServiceProxy : ServiceProxy<WateringDto> , IWateringServiceProxy
    {
        public WateringServiceProxy(IHttpClientFactory httpClientFactory, IConfiguration config)
        : base (httpClientFactory, config, "waterings")
        { 
        }
    }
}
