using System;
using System.Collections.Generic;

namespace TallerAutomotriz.Domain.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int EstimatedTimeInMinutes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Propiedades de navegación
        public ICollection<ServiceOrderDetail> ServiceOrderDetails { get; set; }
    }
}