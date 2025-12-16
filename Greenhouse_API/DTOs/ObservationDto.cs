using Greenhouse_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
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
        public ObservationRating Rating { get; set; }
        [StringLength(500)]
        public string Comments { get; set; } = null!;
    }

}
