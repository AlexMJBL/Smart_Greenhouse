using Greenhouse_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    /// <summary>
    /// Environmental sensor record associated with a zone
    /// </summary>
    public class ZoneRecordDto
    {
        /// <summary>
        /// Unique identifier of the zone record
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Recorded sensor value
        /// </summary>
        public float Record { get; set; }

        /// <summary>
        /// Indicates whether the recorded value is within the acceptable range
        /// </summary>
        public bool InRange { get; set; }

        /// <summary>
        /// Identifier of the zone where the record was taken
        /// </summary>
        public int ZoneId { get; set; }

        /// <summary>
        /// Identifier of the sensor that recorded the value
        /// </summary>
        public int SensorId { get; set; }

        /// <summary>
        /// Type of sensor that produced the record
        /// </summary>
        public SensorType Type { get; set; }

        /// <summary>
        /// Date and time when the record was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Data required to create a new zone sensor record
    /// </summary>
    public class ZoneRecordWriteDto
    {
        /// <summary>
        /// Recorded sensor value
        /// </summary>
        [Required]
        [Range(0, float.MaxValue)]
        public float Record { get; set; }

        /// <summary>
        /// Identifier of the sensor that recorded the value
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int SensorId { get; set; }

        /// <summary>
        /// Type of sensor that produced the record
        /// </summary>
        [Required]
        [EnumDataType(typeof(SensorType))]
        public SensorType Type { get; set; }
    }
}
