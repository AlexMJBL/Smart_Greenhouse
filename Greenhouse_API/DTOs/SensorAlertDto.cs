using Greenhouse_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    public class SensorAlertDto
    {
        public int Id { get; set; }

        public AlertReason Reason { get; set; }

        public int SensorId { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class SensorAlertWriteDto
    {
        [Required]
        public AlertReason Reason { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int SensorId { get; set; }
    }
}
