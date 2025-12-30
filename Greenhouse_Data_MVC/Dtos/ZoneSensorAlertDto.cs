using System.ComponentModel.DataAnnotations;
using Greenhouse_Data_MVC.Enums;

namespace Greenhouse_Data_MVC.dtos
{
    public class ZoneSensorAlertDto
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public AlertReason Reason { get; set; }
        public SensorType SensorType { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}