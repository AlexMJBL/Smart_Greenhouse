namespace Greenhouse_Data_MVC.Dtos
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
}