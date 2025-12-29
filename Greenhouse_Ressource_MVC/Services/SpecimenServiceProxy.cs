using Greenhouse_Ressource_MVC.Dtos;

namespace Greenhouse_Ressource_MVC.Services
{
    public class SpecimenServiceProxy : ServiceProxy<SpecimenDto, SpecimenWriteDto>
    {
        public SpecimenServiceProxy(IHttpClientFactory httpClientFactory) 
        :base(httpClientFactory, "api/specimens")
        { }
    }
}
