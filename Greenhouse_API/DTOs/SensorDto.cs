using Greenhouse_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    /// <summary>
    /// Sensor returned by the API
    /// </summary>
    public class SensorDto
    {
        /// <summary>
        /// Unique identifier of the sensor
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique code identifying the sensor
        /// </summary>
        public string SensorCode { get; set; } = null!;

        /// <summary>
        /// Optional description or notes about the sensor
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Type of the sensor
        /// </summary>
        public SensorType Type { get; set; }

        /// <summary>
        /// Indicates whether the sensor was recently seen / reported data
        /// </summary>
        public bool LastSeen { get; set; }

        /// <summary>
        /// Identifier of the zone where the sensor is installed
        /// </summary>
        public int ZoneId { get; set; }

        /// <summary>
        /// Indicates whether the sensor is currently active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Date and time when the sensor was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Data required to create a new sensor
    /// </summary>
    public class SensorWriteDto
    {
        /// <summary>
        /// Unique code identifying the sensor (max 50 characters)
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string SensorCode { get; set; } = null!;

        /// <summary>
        /// Optional description or notes (max 500 characters)
        /// </summary>
        [MaxLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Type of the sensor
        /// </summary>
        [Required]
        [EnumDataType(typeof(SensorType))]
        public SensorType Type { get; set; }

        /// <summary>
        /// Identifier of the zone where the sensor is installed
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int ZoneId { get; set; }
    }
}
