using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    /// <summary>
    /// Environmental zone category returned by the API
    /// </summary>
    public class ZoneCategoryDto
    {
        /// <summary>
        /// Unique identifier of the zone category
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the zone category
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Minimum humidity percentage (0–100)
        /// </summary>
        public float HumidityMinPct { get; set; }

        /// <summary>
        /// Maximum humidity percentage (0–100)
        /// </summary>
        public float HumidityMaxPct { get; set; }

        /// <summary>
        /// Minimum luminosity in lux
        /// </summary>
        public float LuminosityMinLux { get; set; }

        /// <summary>
        /// Maximum luminosity in lux
        /// </summary>
        public float LuminosityMaxLux { get; set; }

        /// <summary>
        /// Minimum temperature in Celsius
        /// </summary>
        public float TemperatureMinC { get; set; }

        /// <summary>
        /// Maximum temperature in Celsius
        /// </summary>
        public float TemperatureMaxC { get; set; }

        /// <summary>
        /// Minimum atmospheric pressure in pascals
        /// </summary>
        public float? PressureMinPa { get; set; }

        /// <summary>
        /// Maximum atmospheric pressure in pascals
        /// </summary>
        public float? PressureMaxPa { get; set; }

        /// <summary>
        /// Date and time when the zone category was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Data required to create a new zone category
    /// </summary>
    public class ZoneCategoryWriteDto : IValidatableObject
    {
        /// <summary>
        /// Name of the zone category (max 100 characters)
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Minimum humidity percentage (0–100)
        /// </summary>
        [Required]
        [Range(0, 100)]
        public float HumidityMinPct { get; set; }

        /// <summary>
        /// Maximum humidity percentage (0–100)
        /// </summary>
        [Required]
        [Range(0, 100)]
        public float HumidityMaxPct { get; set; }

        /// <summary>
        /// Minimum luminosity in lux
        /// </summary>
        [Required]
        [Range(0, float.MaxValue)]
        public float LuminosityMinLux { get; set; }

        /// <summary>
        /// Maximum luminosity in lux
        /// </summary>
        [Required]
        [Range(0, float.MaxValue)]
        public float LuminosityMaxLux { get; set; }

        /// <summary>
        /// Minimum temperature in Celsius
        /// </summary>
        [Required]
        [Range(0, float.MaxValue)]
        public float TemperatureMinC { get; set; }

        /// <summary>
        /// Maximum temperature in Celsius
        /// </summary>
        [Required]
        [Range(0, float.MaxValue)]
        public float TemperatureMaxC { get; set; }

        /// <summary>
        /// Minimum atmospheric pressure in pascals
        /// </summary>
        [Required]
        [Range(0, float.MaxValue)]
        public float? PressureMinPa { get; set; }

        /// <summary>
        /// Maximum atmospheric pressure in pascals
        /// </summary>
        [Required]
        [Range(0, float.MaxValue)]
        public float? PressureMaxPa { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HumidityMaxPct <= HumidityMinPct)
            {
                yield return new ValidationResult(
                    "L'humidité maximale doit être plus grande que l'humidité minimale.",
                    new[] { nameof(HumidityMaxPct) }
                );
            }

            if (LuminosityMaxLux <= LuminosityMinLux)
            {
                yield return new ValidationResult(
                    "La luminosité maximale doit être plus grande que la luminosité minimale.",
                    new[] { nameof(LuminosityMaxLux) }
                );
            }

            if (TemperatureMaxC <= TemperatureMinC)
            {
                yield return new ValidationResult(
                    "La température maximale doit être plus grande que la température minimale.",
                    new[] { nameof(TemperatureMaxC) }
                );
            }

            if (PressureMinPa.HasValue && PressureMaxPa.HasValue &&
                PressureMaxPa <= PressureMinPa)
            {
                yield return new ValidationResult(
                    "La pression maximale doit être plus grande que la pression minimale.",
                    new[] { nameof(PressureMaxPa) }
                );
            }
        }
    }
}
