using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    public class SoilHumidityCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public float? MinHumidityPct { get; set; }

        public float? MaxHumidityPct { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class SoilHumidityCategoryWriteDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Range(0,100)]
        public float? MinHumidityPct { get; set; }
        [Range(0, 100)]
        public float? MaxHumidityPct { get; set; }
    }
}
