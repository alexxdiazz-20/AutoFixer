using System;

namespace TallerAutomotriz.Domain.Entities
{
    public class ServiceOrderDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Claves foráneas
        public int ServiceOrderId { get; set; }
        public int ServiceId { get; set; }

        // Propiedades de navegación
        public ServiceOrder ServiceOrder { get; set; }
        public Service Service { get; set; }
    }
}