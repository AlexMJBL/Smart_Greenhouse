using Greenhouse_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    /// <summary>
    /// Observation returned by the API
    /// </summary>
    public class ObservationDto
    {
        /// <summary>
        /// Unique identifier of the observation
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identifier of the observed plant
        /// </summary>
        public int PlantId { get; set; }

        /// <summary>
        /// Rating representing the plant condition
        /// </summary>
        public ObservationRating Rating { get; set; }

        /// <summary>
        /// Optional comments about the observation
        /// </summary>
        public string Comments { get; set; } = null!;

        /// <summary>
        /// Date when the observation was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Data required to create a new observation
    /// </summary>
    public class ObservationWriteDto
    {
        /// <summary>
        /// Identifier of the observed plant
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int PlantId { get; set; }

        /// <summary>
        /// Rating representing the plant condition
        /// </summary>
        [Required]
        [EnumDataType(typeof(ObservationRating))]
        public ObservationRating Rating { get; set; }

        /// <summary>
        /// Optional comments (maximum 500 characters)
        /// </summary>
        [MaxLength(500)]
        public string Comments { get; set; } = null!;
    }
}
