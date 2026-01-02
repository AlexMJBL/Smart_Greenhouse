using System.ComponentModel.DataAnnotations;

namespace Greenhouse_Ressource_MVC.Dtos
{
    public class SoilHumidityCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float MinHumidityPct { get; set; }
        public float MaxHumidityPct { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class SoilHumidityCategoryWriteDto : IValidatableObject
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [Range(0, 100)]
        public float MinHumidityPct { get; set; }
        [Required]
        [Range(0, 100)]
        public float MaxHumidityPct { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MaxHumidityPct <= MinHumidityPct)
            {
                yield return new ValidationResult(
                    "The maximum need to be higher than minimum",
                    new[] { nameof(MaxHumidityPct) }
                );
            }
        }
    }
}
