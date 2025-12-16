using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    public class PlantDto
    {
        public int Id { get; set; }
        [Required]
        public DateOnly AcquiredDate { get; set; }
        public int SpecimenId { get; set; }
        public int ZoneId { get; set; }
        public int? MomId { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class PlantWriteDto
    {
        [Required]
        public DateOnly AcquiredDate { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ZoneId { get; set; }
        public int? SpecimenId { get; set; }
        public int? MomId { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
    }

}
