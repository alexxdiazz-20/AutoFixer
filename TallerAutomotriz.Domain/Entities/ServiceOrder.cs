using System;
using System.Collections.Generic;

namespace TallerAutomotriz.Domain.Entities
{
    public enum ServiceOrderStatus
    {
        Pending,
        InProgress,
        Completed,
        Cancelled
    }

    public class ServiceOrder
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Description { get; set; }
        public ServiceOrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Claves foráneas
        public int VehicleId { get; set; }
        public int MechanicId { get; set; }

        // Propiedades de navegación
        public Vehicle Vehicle { get; set; }
        public Mechanic Mechanic { get; set; }
        public ICollection<ServiceOrderDetail> Details { get; set; }
    }
}