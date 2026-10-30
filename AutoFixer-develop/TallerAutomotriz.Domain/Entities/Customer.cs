using System;
using System.Collections.Generic;

namespace TallerAutomotriz.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Propiedades de navegación
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}