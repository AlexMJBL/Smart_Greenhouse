
using Greenhouse_Ressource_MVC.Dtos;

namespace Greenhouse_Ressource_MVC.ViewModel
{
    public class PlantDetailViewModel
    {
        public PlantDto Plant { get; set; }
        public SpecimenDto Specimen{ get; set; }
        public ZoneDto Zone {get;set;}
        public PlantDto? momPlant { get; set; }
    }
}