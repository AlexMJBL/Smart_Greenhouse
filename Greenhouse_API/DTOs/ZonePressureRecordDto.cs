using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    /// <summary>
    /// Atmospheric pressure record associated with a zone
    /// </summary>
    public class ZonePressureRecordDto
    {
        /// <summary>
        /// Unique identifier of the pressure record
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Recorded atmospheric pressure in hectopascals (hPa)
        /// </summary>
        public float RecordedHPa { get; set; }

        /// <summary>
        /// Identifier of the zone where the pressure was recorded
        /// </summary>
        public int ZoneId { get; set; }

        /// <summary>
        /// Identifier of the sensor that recorded the pressure
        /// </summary>
        public int SensorId { get; set; }

        /// <summary>
        /// Date and time when the pressure record was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Data required to create a new zone pressure record
    /// </summary>
    public class ZonePressureRecordWriteDto
    {
        /// <summary>
        /// Recorded atmospheric pressure in hectopascals (hPa)
        /// </summary>
        [Required]
        [Range(0, float.MaxValue)]
        public float RecordedHPa { get; set; }

        /// <summary>
        /// Identifier of the zone where the pressure was recorded
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int ZoneId { get; set; }

        /// <summary>
        /// Identifier of the sensor that recorded the pressure
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int SensorId { get; set; }
    }
}
