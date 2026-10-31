using System.ComponentModel.DataAnnotations;

namespace TallerAutomotriz.Application.DTOs
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int EstimatedTimeInMinutes { get; set; }
    }

    public class CreateServiceDto
    {
        [Required(ErrorMessage = "El nombre del servicio es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Range(typeof(decimal), "0.01", "999999999",
            ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue,
            ErrorMessage = "El tiempo estimado debe ser mayor que cero.")]
        public int EstimatedTimeInMinutes { get; set; }
    }

    public class UpdateServiceDto
    {
        [Required(ErrorMessage = "El nombre del servicio es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Range(typeof(decimal), "0.01", "999999999",
            ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue,
            ErrorMessage = "El tiempo estimado debe ser mayor que cero.")]
        public int EstimatedTimeInMinutes { get; set; }
    }
}