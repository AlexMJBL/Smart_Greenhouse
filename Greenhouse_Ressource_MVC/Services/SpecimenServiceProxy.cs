using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Interfaces;

namespace Greenhouse_Ressource_MVC.Services
{
    public class SpecimenServiceProxy : ServiceProxy<SpecimenDto, SpecimenWriteDto>, ISpecimenServiceProxy
    {
        public SpecimenServiceProxy(IHttpClientFactory httpClientFactory, IConfiguration config) 
        :base(httpClientFactory, config, "specimens")
        { }
    }
}
