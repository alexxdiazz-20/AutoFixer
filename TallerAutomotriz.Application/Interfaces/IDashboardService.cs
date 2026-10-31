using TallerAutomotriz.Application.DTOs;

namespace TallerAutomotriz.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetSummaryAsync();
    }
}
