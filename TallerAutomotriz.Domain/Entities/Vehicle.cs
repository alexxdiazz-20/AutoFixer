
using System;
using System.Collections.Generic;

namespace TallerAutomotriz.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }

        public string LicensePlate { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        // Propiedades opcionales
        public string? Color { get; set; }
        public string? VIN { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Claves foráneas
        public int CustomerId { get; set; }

        // Propiedades de navegación
        public Customer Customer { get; set; } = null!;
        public ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
    }
}
