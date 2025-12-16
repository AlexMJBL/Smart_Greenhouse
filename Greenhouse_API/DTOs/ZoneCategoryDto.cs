using System.ComponentModel.DataAnnotations;

namespace Greenhouse_API.DTOs
{
    public class ZoneCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float HumidityMinPct { get; set; }

        public float HumidityMaxPct { get; set; }

        public float LuminosityMinLux { get; set; }

        public float LuminosityMaxLux { get; set; }

        public float TemperatureMinC { get; set; }

        public float TemperatureMaxC { get; set; }

        public float PressureMinPa { get; set; }

        public float PressureMaxPa { get; set; }

        public DateTime CreatedAt { get; set; }
    }
    public class ZoneCategoryWriteDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [Range(0,100)]
        public float HumidityMinPct { get; set; }
        [Required]
        [Range(0, 100)]
        public float HumidityMaxPct { get; set; }
        [Required]
        [Range(0,float.MaxValue)]
        public float LuminosityMinLux { get; set; }
        [Required]
        [Range(0, float.MaxValue)]
        public float LuminosityMaxLux { get; set; }
        [Required]
        [Range(0, float.MaxValue)]
        public float TemperatureMinC { get; set; }
        [Required]
        [Range(0, float.MaxValue)]
        public float TemperatureMaxC { get; set; }
        [Required]
        [Range(0, float.MaxValue)]
        public float PressureMinPa { get; set; }
        [Required]
        [Range(0, float.MaxValue)]
        public float PressureMaxPa { get; set; }
    }
}
