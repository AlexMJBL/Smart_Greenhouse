using Greenhouse_Ressource_MVC.Enums;
using System.ComponentModel.DataAnnotations;

namespace Greenhouse_Ressource_MVC.Dtos
{
    public class SensorDto
    {
        public int Id { get; set; }
        public string SensorCode { get; set; } = null!;
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
        public string SensorCode { get; set; } = null!;
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required]
        [EnumDataType(typeof(SensorType))]
        public SensorType Type { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ZoneId { get; set; }
    }
}
