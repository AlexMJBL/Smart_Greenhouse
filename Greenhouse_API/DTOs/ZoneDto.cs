using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    public class ZoneDto
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public int ZoneCategoryId { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class ZoneWriteDto
    {
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ZoneCategoryId { get; set; }

    }
}
