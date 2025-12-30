using System.ComponentModel.DataAnnotations;

namespace Greenhouse_Data_MVC.Dtos
{
    public class PlantHumidityRecordDto
    {
        public int Id { get; set; }
        public float RecordPct { get; set; }
        public bool InRange { get; set; }
        public int PlantId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
