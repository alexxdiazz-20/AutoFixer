namespace TallerAutomotriz.Application.DTOs
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string VIN { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }

    public class CreateVehicleDto
    {
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string VIN { get; set; }
        public int CustomerId { get; set; }
    }

    public class UpdateVehicleDto
    {
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string VIN { get; set; }
        public int CustomerId { get; set; }
    }
}