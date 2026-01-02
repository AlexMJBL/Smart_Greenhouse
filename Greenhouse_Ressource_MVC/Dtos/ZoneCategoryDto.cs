using System.ComponentModel.DataAnnotations;

namespace Greenhouse_Ressource_MVC.Dtos
{
    public class ZoneCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float HumidityMinPct { get; set; }
        public float HumidityMaxPct { get; set; }
        public float LuminosityMinLux { get; set; }
        public float LuminosityMaxLux { get; set; }
        public float TemperatureMinC { get; set; }
        public float TemperatureMaxC { get; set; }
        public float? PressureMinPa { get; set; }
        public float? PressureMaxPa { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ZoneCategoryWriteDto : IValidatableObject
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [Range(0, 100)]
        public float HumidityMinPct { get; set; }
        [Required]
        [Range(0, 100)]
        public float HumidityMaxPct { get; set; }
        [Required]
        [Range(0, float.MaxValue)]
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
        [Range(0, float.MaxValue)]
        public float? PressureMinPa { get; set; }
        [Range(0, float.MaxValue)]
        public float? PressureMaxPa { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HumidityMaxPct <= HumidityMinPct)
            {
                yield return new ValidationResult(
                    "L'humidité maximale doit être plus grande que l'humidité minimale.",
                    new[] { nameof(HumidityMaxPct) }
                );
            }

            if (LuminosityMaxLux <= LuminosityMinLux)
            {
                yield return new ValidationResult(
                    "La luminosité maximale doit être plus grande que la luminosité minimale.",
                    new[] { nameof(LuminosityMaxLux) }
                );
            }

            if (TemperatureMaxC <= TemperatureMinC)
            {
                yield return new ValidationResult(
                    "La température maximale doit être plus grande que la température minimale.",
                    new[] { nameof(TemperatureMaxC) }
                );
            }

            if (PressureMinPa.HasValue && PressureMaxPa.HasValue &&
                PressureMaxPa <= PressureMinPa)
            {
                yield return new ValidationResult(
                    "La pression maximale doit être plus grande que la pression minimale.",
                    new[] { nameof(PressureMaxPa) }
                );
            }
        }
    }
}
