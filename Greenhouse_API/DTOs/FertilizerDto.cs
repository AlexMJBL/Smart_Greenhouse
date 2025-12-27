using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    /// <summary>
    /// Represents a fertilizer returned by the API
    /// </summary>
    public class FertilizerDto
    {
        /// <summary>
        /// Fertilizer unique identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Fertilizer type
        /// </summary>
        /// <example>20-20-20</example>
        public string Type { get; set; } = null!;

        /// <summary>
        /// Identifier of the plant associated with the fertilizer
        /// </summary>
        /// <example>3</example>
        public int PlantId { get; set; }

        /// <summary>
        /// Fertilizer creation date (UTC)
        /// </summary>
        /// <example>2024-12-01T14:30:00Z</example>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Represents the data required to create or update a fertilizer
    /// </summary>
    public class FertilizerWriteDto
    {
        /// <summary>
        /// Fertilizer type
        /// </summary>
        /// <example>20-20-20</example>
        [Required]
        [StringLength(50)]
        public string Type { get; set; } = null!;

        /// <summary>
        /// Identifier of the plant associated with the fertilizer
        /// </summary>
        /// <example>2</example>
        [Required]
        [Range(1, int.MaxValue)]
        public int PlantId { get; set; }
    }
}
