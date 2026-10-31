using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TallerAutomotriz.Domain.Entities;

namespace TallerAutomotriz.Application.DTOs
{
    public class ServiceOrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string? Description { get; set; }
        public ServiceOrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public int VehicleId { get; set; }
        public string VehicleLicensePlate { get; set; } = string.Empty;
        public string VehicleBrandModel { get; set; } = string.Empty;
        public int MechanicId { get; set; }
        public string MechanicFullName { get; set; } = string.Empty;
        public List<ServiceOrderDetailDto> Details { get; set; } = new();
    }

    public class CreateServiceOrderDto : IValidatableObject
    {
        [Required(ErrorMessage = "La fecha de la orden es obligatoria.")]
        public DateTime OrderDate { get; set; }

        public string? Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un vehículo.")]
        public int VehicleId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un mecánico.")]
        public int MechanicId { get; set; }

        [Required(ErrorMessage = "Debe agregar al menos un servicio.")]
        [MinLength(1, ErrorMessage = "Debe agregar al menos un servicio.")]
        public List<CreateServiceOrderDetailDto> Details { get; set; } = new();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (OrderDate == default)
            {
                yield return new ValidationResult(
                    "La fecha de la orden es obligatoria.",
                    new[] { nameof(OrderDate) });
            }
        }
    }

    public class UpdateServiceOrderDto
    {
        public DateTime? CompletionDate { get; set; }
        public string? Description { get; set; }
        public ServiceOrderStatus Status { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un vehículo.")]
        public int VehicleId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un mecánico.")]
        public int MechanicId { get; set; }

        [Required(ErrorMessage = "Debe agregar al menos un servicio.")]
        [MinLength(1, ErrorMessage = "Debe agregar al menos un servicio.")]
        public List<CreateServiceOrderDetailDto> Details { get; set; } = new();
    }

    public class ServiceOrderDetailDto
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public string? Notes { get; set; }
    }

    public class CreateServiceOrderDetailDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un servicio.")]
        public int ServiceId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser como mínimo 1.")]
        public int Quantity { get; set; }

        public string? Notes { get; set; }
    }
}