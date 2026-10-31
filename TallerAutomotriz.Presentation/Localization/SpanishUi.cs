using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using TallerAutomotriz.Domain.Entities;

namespace TallerAutomotriz.Presentation.Localization;

public static class SpanishUi
{
    public static string GetStatusName(ServiceOrderStatus status) => status switch
    {
        ServiceOrderStatus.Pending => "Pendiente",
        ServiceOrderStatus.InProgress => "En progreso",
        ServiceOrderStatus.Completed => "Completada",
        ServiceOrderStatus.Cancelled => "Cancelada",
        _ => "Desconocido"
    };

    public static IEnumerable<SelectListItem> StatusOptions =>
        Enum.GetValues<ServiceOrderStatus>().Select(status => new SelectListItem(
            GetStatusName(status), ((int)status).ToString(CultureInfo.InvariantCulture)));
}
