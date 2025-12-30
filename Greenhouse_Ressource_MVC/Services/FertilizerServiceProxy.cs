using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Interfaces;

namespace Greenhouse_Ressource_MVC.Services
{
    public class FertilizerServiceProxy : ServiceProxy<FertilizerDto,FertilizerWriteDto> , IFertilizerServiceProxy
    {
        public FertilizerServiceProxy(IHttpClientFactory httpClientFactory, IConfiguration config)
        : base(httpClientFactory, config, "fertilizers")
        {
        }
    }
}
