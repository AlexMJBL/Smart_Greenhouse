using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    /// <summary>
    /// Plant returned by the API
    /// </summary>
    public class PlantDto
    {
        /// <summary>
        /// Unique identifier of the plant
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date when the plant was acquired
        /// </summary>
        public DateOnly AcquiredDate { get; set; }

        /// <summary>
        /// Identifier of the specimen type
        /// </summary>
        public int SpecimenId { get; set; }

        /// <summary>
        /// Identifier of the zone where the plant is located
        /// </summary>
        public int ZoneId { get; set; }

        /// <summary>
        /// Identifier of the mother plant (if applicable)
        /// </summary>
        public int? MomId { get; set; }

        /// <summary>
        /// Optional description or notes about the plant
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Indicates whether the plant is currently active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Date and time when the plant was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Data required to create a new plant
    /// </summary>
    public class PlantWriteDto
    {
        /// <summary>
        /// Date when the plant was acquired
        /// </summary>
        [Required]
        public DateOnly AcquiredDate { get; set; }

        /// <summary>
        /// Identifier of the zone where the plant is located
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int ZoneId { get; set; }

        /// <summary>
        /// Identifier of the specimen type
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int SpecimenId { get; set; }

        /// <summary>
        /// Identifier of the mother plant (optional)
        /// </summary>
        public int? MomId { get; set; }

        /// <summary>
        /// Optional description or notes (maximum 500 characters)
        /// </summary>
        [StringLength(500)]
        public string? Description { get; set; }
    }
}
