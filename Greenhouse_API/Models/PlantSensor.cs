using Greenhouse_API.Enums;

namespace Greenhouse_API.Models
{
    public class PlantSensor
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public SensorType Type { get; set; }
        public int PlantId { get; set; }
        public Plant Plant { get; set; } = null!;
    }
}
