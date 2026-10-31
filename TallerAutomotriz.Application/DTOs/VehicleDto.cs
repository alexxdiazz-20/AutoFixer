
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TallerAutomotriz.Application.DTOs
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string? Color { get; set; }
        public string? VIN { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
    }

    public class CreateVehicleDto : IValidatableObject
    {
        [Required(ErrorMessage = "La matrícula es obligatoria.")]
        [StringLength(20, ErrorMessage = "La matrícula no puede superar los 20 caracteres.")]
        public string LicensePlate { get; set; } = string.Empty;

        [Required(ErrorMessage = "La marca es obligatoria.")]
        [StringLength(50, ErrorMessage = "La marca no puede superar los 50 caracteres.")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "El modelo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El modelo no puede superar los 50 caracteres.")]
        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        public string? Color { get; set; }

        [StringLength(50, ErrorMessage = "El VIN no puede superar los 50 caracteres.")]
        public string? VIN { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un cliente.")]
        public int CustomerId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Year < 1950 || Year > DateTime.Now.Year + 1)
            {
                yield return new ValidationResult(
                    $"El año debe estar entre 1950 y {DateTime.Now.Year + 1}.",
                    new[] { nameof(Year) });
            }
        }
    }

    public class UpdateVehicleDto : IValidatableObject
    {
        [Required(ErrorMessage = "La matrícula es obligatoria.")]
        [StringLength(20, ErrorMessage = "La matrícula no puede superar los 20 caracteres.")]
        public string LicensePlate { get; set; } = string.Empty;

        [Required(ErrorMessage = "La marca es obligatoria.")]
        [StringLength(50, ErrorMessage = "La marca no puede superar los 50 caracteres.")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "El modelo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El modelo no puede superar los 50 caracteres.")]
        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        public string? Color { get; set; }

        [StringLength(50, ErrorMessage = "El VIN no puede superar los 50 caracteres.")]
        public string? VIN { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un cliente.")]
        public int CustomerId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Year < 1950 || Year > DateTime.Now.Year + 1)
            {
                yield return new ValidationResult(
                    $"El año debe estar entre 1950 y {DateTime.Now.Year + 1}.",
                    new[] { nameof(Year) });
            }
        }
    }
}
