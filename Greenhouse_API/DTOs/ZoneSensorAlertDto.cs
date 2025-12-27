using Greenhouse_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    /// <summary>
    /// Sensor alert generated for a zone
    /// </summary>
    public class ZoneSensorAlertDto
    {
        /// <summary>
        /// Unique identifier of the sensor alert
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identifier of the sensor associated with the alert
        /// </summary>
        public int SensorId { get; set; }

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
    /// Data required to create a new zone sensor alert
    /// </summary>
    public class ZoneSensorAlertPartial
    {
        /// <summary>
        /// Identifier of the sensor associated with the alert
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int SensorId { get; set; }

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
