using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    public class FertilizerDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public int PlantId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class FertilizerWriteDto
    {
        [Required]
        [StringLength(50)]
        public string Type { get; set; } = null!;
        [Required]
        [Range(1, int.MaxValue)]
        public int PlantId { get; set; }
    }
}
