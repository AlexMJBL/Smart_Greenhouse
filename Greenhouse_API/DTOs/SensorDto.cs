using Greenhouse_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    public class SensorDto
    {
        public int Id { get; set; }
        public string SensorCode { get; set; }

        public string? Description { get; set; }

        public SensorType Type { get; set; } 

        public bool LastSeen { get; set; }

        public int ZoneId { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class SensorWriteDto
    {
        [Required]
        [MaxLength(50)]
        public string SensorCode { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required]
        public SensorType Type { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ZoneId { get; set; }

    }
}
