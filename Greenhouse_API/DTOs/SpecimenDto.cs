using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    public class SpecimenDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int SoilHumidityCatId { get; set; }
    }

    public class SpecimenCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        [Range(1, int.MaxValue)]
        public int SoilHumidityCatId { get; set; }
    }
}
