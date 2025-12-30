using System.ComponentModel.DataAnnotations;
using Greenhouse_Data_MVC.Enums;

namespace Greenhouse_Data_MVC.DTOs
{
     public class ZoneRecordDto
    {
        public int Id { get; set; }
        public float Record { get; set; }
        public bool InRange { get; set; }
        public int ZoneId { get; set; }
        public int SensorId { get; set; }
        public SensorType Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}