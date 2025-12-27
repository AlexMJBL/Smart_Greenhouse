using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    /// <summary>
    /// Specimen returned by the API
    /// </summary>
    public class SpecimenDto
    {
        /// <summary>
        /// Unique identifier of the specimen
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the specimen
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Identifier of the associated soil humidity category
        /// </summary>
        public int SoilHumidityCatId { get; set; }

        /// <summary>
        /// Date and time when the specimen was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Data required to create a new specimen
    /// </summary>
    public class SpecimenWriteDto
    {
        /// <summary>
        /// Name of the specimen
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Identifier of the associated soil humidity category
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int SoilHumidityCatId { get; set; }
    }
}
