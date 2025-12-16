using Greenhouse_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    public class ZoneRecordDto
    {
        public int Id { get; set; }

        public float Record { get; set; }

        public bool InRange { get; set; }

        public int ZoneId { get; set; }

        public string SensorId { get; set; } 

        public SensorType Type { get; set; } 

        public DateTime CreatedAt { get; set; }
    }
    public class ZoneRecordWriteDto
    {
        [Required]
        [Range(0, float.MaxValue)]
        public float Record { get; set; }
        [Required]
        public bool InRange { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public int ZoneId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public string SensorId { get; set; }
        [Required]
        public SensorType Type { get; set; }
    }
}
