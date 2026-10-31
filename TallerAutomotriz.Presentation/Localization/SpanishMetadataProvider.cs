using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace TallerAutomotriz.Presentation.Localization;

public sealed class SpanishMetadataProvider : IDisplayMetadataProvider, IValidationMetadataProvider
{
    private static readonly IReadOnlyDictionary<string, string> Labels = new Dictionary<string, string>
    {
        ["Id"] = "Código", ["Name"] = "Nombre", ["LastName"] = "Apellido",
        ["Email"] = "Correo electrónico", ["Phone"] = "Teléfono", ["Address"] = "Dirección",
        ["LicensePlate"] = "Placa", ["Brand"] = "Marca", ["Model"] = "Modelo",
        ["Year"] = "Año", ["Color"] = "Color", ["VIN"] = "Número de chasis (VIN)",
        ["CustomerId"] = "Cliente", ["CustomerName"] = "Cliente",
        ["Specialization"] = "Especialidad", ["HireDate"] = "Fecha de contratación",
        ["Description"] = "Descripción", ["Price"] = "Precio",
        ["EstimatedTimeInMinutes"] = "Tiempo estimado (minutos)",
        ["OrderDate"] = "Fecha de la orden", ["CompletionDate"] = "Fecha de finalización",
        ["Status"] = "Estado", ["TotalAmount"] = "Total",
        ["VehicleId"] = "Vehículo", ["VehicleLicensePlate"] = "Placa",
        ["VehicleBrandModel"] = "Marca y modelo", ["MechanicId"] = "Mecánico",
        ["MechanicFullName"] = "Mecánico", ["ServiceId"] = "Servicio", ["ServiceName"] = "Servicio",
        ["Quantity"] = "Cantidad", ["UnitPrice"] = "Precio unitario",
        ["Subtotal"] = "Subtotal", ["Notes"] = "Notas"
    };

    public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
    {
        if (context.Key.Name is string name && Labels.TryGetValue(name, out var label))
            context.DisplayMetadata.DisplayName = () => label;

        var type = Nullable.GetUnderlyingType(context.Key.ModelType) ?? context.Key.ModelType;
        if (type == typeof(DateTime))
            context.DisplayMetadata.DisplayFormatString = "{0:dd/MM/yyyy}";
        else if (type == typeof(decimal))
            context.DisplayMetadata.DisplayFormatString = "{0:C2}";
    }

    public void CreateValidationMetadata(ValidationMetadataProviderContext context)
    {
        foreach (var attribute in context.ValidationMetadata.ValidatorMetadata.OfType<RequiredAttribute>())
        {
            if (attribute.ErrorMessage == null && attribute.ErrorMessageResourceName == null)
                attribute.ErrorMessage = "El campo {0} es obligatorio.";
        }
    }
}
