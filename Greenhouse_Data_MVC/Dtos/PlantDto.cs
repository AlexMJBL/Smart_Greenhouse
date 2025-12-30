namespace Greenhouse_Data_MVC.Dtos
{
    public class PlantDto
    {
        public int Id { get; set; }
        public DateOnly AcquiredDate { get; set; }
        public int SpecimenId { get; set; }
        public int ZoneId { get; set; }
        public int? MomId { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}