using Greenhouse_Ressource_MVC.Enums;
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

    public class ObservationWriteDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int PlantId { get; set; }
        [Required]
        [EnumDataType(typeof(ObservationRating))]
        public ObservationRating Rating { get; set; }
        [MaxLength(500)]
        public string Comments { get; set; } = null!;
    }
}
