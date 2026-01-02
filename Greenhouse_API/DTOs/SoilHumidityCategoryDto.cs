using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    /// <summary>
    /// Soil humidity category returned by the API
    /// </summary>
    public class SoilHumidityCategoryDto
    {
        /// <summary>
        /// Unique identifier of the soil humidity category
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the soil humidity category
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Minimum acceptable soil humidity percentage (0–100)
        /// </summary>
        public float MinHumidityPct { get; set; }

        /// <summary>
        /// Maximum acceptable soil humidity percentage (0–100)
        /// </summary>
        public float MaxHumidityPct { get; set; }

        /// <summary>
        /// Date and time when the category was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Data required to create a new soil humidity category
    /// </summary>
    public class SoilHumidityCategoryWriteDto : IValidatableObject
    {
        /// <summary>
        /// Name of the soil humidity category
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Minimum acceptable soil humidity percentage (0–100)
        /// </summary>
        [Required]
        [Range(0, 100)]
        public float MinHumidityPct { get; set; }

        /// <summary>
        /// Maximum acceptable soil humidity percentage (0–100)
        /// </summary>
        [Required]
        [Range(0, 100)]
        public float MaxHumidityPct { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MaxHumidityPct <= MinHumidityPct)
            {
                yield return new ValidationResult(
                    "The maximum need to be higher than minimum",
                    new[] { nameof(MaxHumidityPct) }
                );
            }
        }
    }
}
