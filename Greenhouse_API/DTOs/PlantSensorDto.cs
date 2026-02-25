using Greenhouse_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    public class PlantSensorDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public SensorType Type { get; set; }

        public int PlantId { get; set; }
    }

    public class PlantSensorWriteDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public SensorType Type { get; set; }

        [Required]
        public int PlantId { get; set; }
    }
}
