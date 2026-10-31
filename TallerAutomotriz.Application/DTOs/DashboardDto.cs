namespace TallerAutomotriz.Application.DTOs
{
    public class DashboardDto
    {
        public DateTime MonthStart { get; set; }
        public int PendingOrders { get; set; }
        public int InProgressOrders { get; set; }
        public int CompletedOrdersThisMonth { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public IReadOnlyList<ServiceOrderDto> RecentOrders { get; set; } = Array.Empty<ServiceOrderDto>();
    }
}
