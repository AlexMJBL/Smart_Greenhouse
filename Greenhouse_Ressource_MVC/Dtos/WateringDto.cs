using System.ComponentModel.DataAnnotations;

namespace Greenhouse_Ressource_MVC.Dtos
{
    public class WateringDto
    {
        public int Id { get; set; }
        public float HumPctBefore { get; set; }
        public float HumPctAfter { get; set; }
        public int WaterQuantityMl { get; set; }
        public int PlantId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class WateringWriteDto
    {
        [Required]
        [Range(0, 100)]
        public float HumPctBefore { get; set; }
        [Required]
        [Range(0, 100)]
        public float HumPctAfter { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int WaterQuantityMl { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int PlantId { get; set; }
    }
}
