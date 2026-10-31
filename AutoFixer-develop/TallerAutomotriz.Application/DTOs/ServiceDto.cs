namespace TallerAutomotriz.Application.DTOs
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int EstimatedTimeInMinutes { get; set; }
    }

    public class CreateServiceDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int EstimatedTimeInMinutes { get; set; }
    }

    public class UpdateServiceDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int EstimatedTimeInMinutes { get; set; }
    }
}