using System;
using System.Collections.Generic;

namespace TallerAutomotriz.Domain.Entities
{
    public class Mechanic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Propiedades de navegación
        public ICollection<ServiceOrder> ServiceOrders { get; set; }
    }
}
