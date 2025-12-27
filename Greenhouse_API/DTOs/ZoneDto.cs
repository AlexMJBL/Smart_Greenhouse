using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    /// <summary>
    /// Zone returned by the API
    /// </summary>
    public class ZoneDto
    {
        /// <summary>
        /// Unique identifier of the zone
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Optional description or notes about the zone
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Identifier of the associated zone category
        /// </summary>
        public int ZoneCategoryId { get; set; }

        /// <summary>
        /// Date and time when the zone was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Data required to create a new zone
    /// </summary>
    public class ZoneWriteDto
    {
        /// <summary>
        /// Optional description or notes (max 500 characters)
        /// </summary>
        [MaxLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Identifier of the associated zone category
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int ZoneCategoryId { get; set; }
    }
}
