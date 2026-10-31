
using System;
using System.ComponentModel.DataAnnotations;

namespace TallerAutomotriz.Application.DTOs
{
    public class MechanicDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Specialization { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
    }

    public class CreateMechanicDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
        public string LastName { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "La especialidad no puede superar los 100 caracteres.")]
        public string? Specialization { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El teléfono no tiene un formato válido.")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de contratación es obligatoria.")]
        [Range(typeof(DateTime), "01/01/1900", "12/31/9999",
            ErrorMessage = "La fecha de contratación no es válida.")]
        public DateTime HireDate { get; set; }
    }

    public class UpdateMechanicDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
        public string LastName { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "La especialidad no puede superar los 100 caracteres.")]
        public string? Specialization { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El teléfono no tiene un formato válido.")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de contratación es obligatoria.")]
        [Range(typeof(DateTime), "01/01/1900", "12/31/9999",
            ErrorMessage = "La fecha de contratación no es válida.")]
        public DateTime HireDate { get; set; }
    }
}
