using Greenhouse_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    public class PlantSensorAlertDto
    {
        public int Id { get; set; }

        public int PlantId { get; set; }

        public AlertReason Reason { get; set; }
        public SensorType SensorType { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class PlantSensorAlertPartial
    {

        [Required]
        [Range(1,int.MaxValue)]
        public int PlantId { get; set; }
        [Required]
        public AlertReason Reason { get; set; }
        [Required]
        public SensorType SensorType { get; set; }

    }
}
