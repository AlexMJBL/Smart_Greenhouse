
using Greenhouse_Data_MVC.Enums;

namespace Greenhouse_Data_MVC.Dtos
{
    public class SensorDto
    {
        public int Id { get; set; }
        public string SensorCode { get; set; } = null!;
        public string? Description { get; set; }
        public SensorType Type { get; set; }
        public bool LastSeen { get; set; }
        public int ZoneId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}