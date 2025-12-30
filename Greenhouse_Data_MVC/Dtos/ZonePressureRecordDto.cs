using System.ComponentModel.DataAnnotations;

namespace Greenhouse_Data_MVC.Dtos
{
     public class ZonePressureRecordDto
    {
        public int Id { get; set; }
        public float RecordedHPa { get; set; }
        public int ZoneId { get; set; }
        public int SensorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}