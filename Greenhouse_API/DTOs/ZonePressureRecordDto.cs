using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    public class ZonePressureRecordDto
    {
        public int Id { get; set; }
        public float RecordedHPa { get; set; }
        public int ZoneId { get; set; }
        public int SensorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ZonePressureRecordWriteDto
    {
        [Required]
        [Range(0, float.MaxValue)]
        public float RecordedHPa { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ZoneId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int SensorId { get; set; }
    }
}
