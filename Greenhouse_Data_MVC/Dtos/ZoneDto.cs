namespace Greenhouse_Data_MVC.Dtos
{
    public class ZoneDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int ZoneCategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}