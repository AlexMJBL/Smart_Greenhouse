using Greenhouse_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    public class ZoneSensorAlertDto
    {
        public int Id { get; set; }

        public int SensorId { get; set; }

        public AlertReason Reason { get; set; }

        public SensorType SensorType { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class ZoneSensorAlertPartial
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int SensorId { get; set; }
        [Required]
        public AlertReason Reason { get; set; }
        [Required]
        public SensorType SensorType { get; set; }
    }
}