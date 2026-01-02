namespace Greenhouse_Data_MVC.Dtos
{
        public class FertilizerDto
        {
            public int Id { get; set; }
            public string Type { get; set; }
            public int PlantId { get; set; }
            public DateTime CreatedAt { get; set; }
        }
}