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
    public class ZoneCategoryWriteDto
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
    }
}
