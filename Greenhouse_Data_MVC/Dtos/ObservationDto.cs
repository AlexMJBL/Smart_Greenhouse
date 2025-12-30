using Greenhouse_Data_MVC.Enums;
using System.ComponentModel.DataAnnotations;

namespace Greenhouse_Ressource_MVC.Dtos
{
    public class ObservationDto
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public ObservationRating Rating { get; set; }
        public string Comments { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
