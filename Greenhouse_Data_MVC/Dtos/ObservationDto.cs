using Greenhouse_Data_MVC.Enums;

namespace Greenhouse_Data_MVC.Dtos
{
    public class ObservationDto
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public ObservationRating Rating { get; set; }
        public string Comments { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
