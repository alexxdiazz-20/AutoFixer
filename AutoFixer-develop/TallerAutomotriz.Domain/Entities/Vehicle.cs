using System;
using System.Collections.Generic;

namespace TallerAutomotriz.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string VIN { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Claves foráneas
        public int CustomerId { get; set; }

        // Propiedades de navegación
        public Customer Customer { get; set; }
        public ICollection<ServiceOrder> ServiceOrders { get; set; }
    }
}