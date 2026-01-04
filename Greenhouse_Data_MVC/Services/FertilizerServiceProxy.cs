using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;

namespace Greenhouse_Data_MVC.Services
{
    public class FertilizerServiceProxy : ServiceProxy<FertilizerDto> , IFertilizerServiceProxy
    {
        public FertilizerServiceProxy(IHttpClientFactory httpClientFactory, IConfiguration config)
        : base (httpClientFactory, config, "fertilizers")
        { 
        }
    }
}
