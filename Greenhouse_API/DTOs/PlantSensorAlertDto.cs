using Greenhouse_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    /// <summary>
    /// Sensor alert generated for a plant
    /// </summary>
    public class PlantSensorAlertDto
    {
        /// <summary>
        /// Unique identifier of the sensor alert
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identifier of the plant associated with the alert
        /// </summary>
        public int PlantId { get; set; }

        /// <summary>
        /// Reason why the alert was triggered
        /// </summary>
        public AlertReason Reason { get; set; }

        /// <summary>
        /// Type of sensor that triggered the alert
        /// </summary>
        public SensorType SensorType { get; set; }

        /// <summary>
        /// Date and time when the alert was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Data required to create a new plant sensor alert
    /// </summary>
    public class PlantSensorAlertPartial
    {
        /// <summary>
        /// Identifier of the plant associated with the alert
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int PlantId { get; set; }

        /// <summary>
        /// Reason why the alert was triggered
        /// </summary>
        [Required]
        [EnumDataType(typeof(AlertReason))]
        public AlertReason Reason { get; set; }

        /// <summary>
        /// Type of sensor that triggered the alert
        /// </summary>
        [Required]
        [EnumDataType(typeof(SensorType))]
        public SensorType SensorType { get; set; }
    }
}
