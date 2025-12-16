using Greenhouse_API.Enums;

namespace Greenhouse_API.DTOs
{
    public class SensorDto
    {
        public string Id { get; set; }
        public string SensorCode { get; set; }

        public string? Description { get; set; }

        public SensorType Type { get; set; } 

        public bool LastSeen { get; set; }

        public int ZoneId { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class SensorWriteDto
    {
        public string SensorCode { get; set; }
        public string? Description { get; set; }

        public SensorType Type { get; set; }

        public int ZoneId { get; set; }

    }
}
