using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    public class PlantAlertDto
    {
        public int Id { get; set; }

        public int PlantId { get; set; }

        public string Reason { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
