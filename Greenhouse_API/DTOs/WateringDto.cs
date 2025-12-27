using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    /// <summary>
    /// Watering event recorded for a plant
    /// </summary>
    public class WateringDto
    {
        /// <summary>
        /// Unique identifier of the watering record
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Soil humidity percentage before watering (0–100)
        /// </summary>
        public float HumPctBefore { get; set; }

        /// <summary>
        /// Soil humidity percentage after watering (0–100)
        /// </summary>
        public float HumPctAfter { get; set; }

        /// <summary>
        /// Quantity of water applied, in milliliters
        /// </summary>
        public int WaterQuantityMl { get; set; }

        /// <summary>
        /// Identifier of the watered plant
        /// </summary>
        public int PlantId { get; set; }

        /// <summary>
        /// Date and time when the watering was recorded
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Data required to record a watering event
    /// </summary>
    public class WateringWriteDto
    {
        /// <summary>
        /// Soil humidity percentage before watering (0–100)
        /// </summary>
        [Required]
        [Range(0, 100)]
        public float HumPctBefore { get; set; }

        /// <summary>
        /// Soil humidity percentage after watering (0–100)
        /// </summary>
        [Required]
        [Range(0, 100)]
        public float HumPctAfter { get; set; }

        /// <summary>
        /// Quantity of water applied, in milliliters
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int WaterQuantityMl { get; set; }

        /// <summary>
        /// Identifier of the watered plant
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int PlantId { get; set; }
    }
}
