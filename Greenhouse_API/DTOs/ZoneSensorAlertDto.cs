using Greenhouse_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    public class ZoneSensorAlertDto
    {
        public int Id { get; set; }

        public int ZoneId { get; set; }

        public AlertReason Reason { get; set; }

        public SensorType SensorType { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class ZoneSensorAlertWriteDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ZoneId { get; set; }
        [Required]
        public AlertReason Reason { get; set; }
        [Required]
        public SensorType SensorType { get; set; }
    }
}
