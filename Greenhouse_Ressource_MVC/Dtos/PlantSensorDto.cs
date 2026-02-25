using Greenhouse_Ressource_MVC.Enums;
using System.ComponentModel.DataAnnotations;

namespace Greenhouse_Ressource_MVC.Dtos
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
        [Range(1, int.MaxValue)]
        public int PlantId { get; set; }
    }
}