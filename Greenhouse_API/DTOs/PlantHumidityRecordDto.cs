using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    public class PlantHumidityRecordDto
    {
        public int Id { get; set; }

        public float RecordPct { get; set; }

        public bool InRange { get; set; }

        public int PlantId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
    public class PlantHumidityWriteDto
    {
        [Required]
        [Range(0,100)]
        public float RecordPct { get; set; }
        [Required]
        public bool InRange { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int PlantId { get; set; }
    }
}
