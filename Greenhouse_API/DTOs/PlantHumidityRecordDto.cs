using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    /// <summary>
    /// Humidity record associated with a plant
    /// </summary>
    public class PlantHumidityRecordDto
    {
        /// <summary>
        /// Unique identifier of the humidity record
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Recorded humidity percentage (0–100)
        /// </summary>
        public float RecordPct { get; set; }

        /// <summary>
        /// Indicates whether the recorded humidity is within the acceptable range
        /// </summary>
        public bool InRange { get; set; }

        /// <summary>
        /// Identifier of the associated plant
        /// </summary>
        public int PlantId { get; set; }

        /// <summary>
        /// Date and time when the record was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Data required to create a new plant humidity record
    /// </summary>
    public class PlantHumidityRecordWriteDto
    {
        /// <summary>
        /// Recorded humidity percentage (0–100)
        /// </summary>
        [Required]
        [Range(0, 100)]
        public float RecordPct { get; set; }

        /// <summary>
        /// Identifier of the associated plant
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int PlantId { get; set; }
    }
}
