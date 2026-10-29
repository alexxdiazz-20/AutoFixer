using System;
using System.Collections.Generic;
using TallerAutomotriz.Domain.Entities;

namespace TallerAutomotriz.Application.DTOs
{
    public class ServiceOrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Description { get; set; }
        public ServiceOrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public int VehicleId { get; set; }
        public string VehicleLicensePlate { get; set; }
        public string VehicleBrandModel { get; set; }
        public int MechanicId { get; set; }
        public string MechanicFullName { get; set; }
        public List<ServiceOrderDetailDto> Details { get; set; }
    }

    public class CreateServiceOrderDto
    {
        public DateTime OrderDate { get; set; }
        public string Description { get; set; }
        public int VehicleId { get; set; }
        public int MechanicId { get; set; }
        public List<CreateServiceOrderDetailDto> Details { get; set; } = new();
    }

    public class UpdateServiceOrderDto
    {
        public DateTime? CompletionDate { get; set; }
        public string Description { get; set; }
        public ServiceOrderStatus Status { get; set; }
        public int VehicleId { get; set; }
        public int MechanicId { get; set; }
        public List<CreateServiceOrderDetailDto> Details { get; set; } = new();
    }

    public class ServiceOrderDetailDto
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public string Notes { get; set; }
    }

    public class CreateServiceOrderDetailDto
    {
        public int ServiceId { get; set; }
        public int Quantity { get; set; }
        // Opcional: si viene vacía se guarda como texto vacío (la columna en BD es NOT NULL)
        public string? Notes { get; set; }
    }
}
