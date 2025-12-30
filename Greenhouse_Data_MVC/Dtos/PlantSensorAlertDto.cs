using System.ComponentModel.DataAnnotations;
using Greenhouse_Data_MVC.Enums;

namespace Greenhouse_Data_MVC.Dtos
{
    public class PlantSensorAlertDto
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public AlertReason Reason { get; set; }
        public SensorType SensorType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}