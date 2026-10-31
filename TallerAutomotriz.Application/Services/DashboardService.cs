using TallerAutomotriz.Application.DTOs;
using TallerAutomotriz.Application.Interfaces;
using TallerAutomotriz.Domain.Entities;

namespace TallerAutomotriz.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IServiceOrderService _serviceOrderService;
        private readonly TimeProvider _timeProvider;

        public DashboardService(IServiceOrderService serviceOrderService, TimeProvider timeProvider)
        {
            _serviceOrderService = serviceOrderService;
            _timeProvider = timeProvider;
        }

        public async Task<DashboardDto> GetSummaryAsync()
        {
            var today = _timeProvider.GetLocalNow().Date;
            var monthStart = new DateTime(today.Year, today.Month, 1);
            var nextMonthStart = monthStart.AddMonths(1);
            var orders = (await _serviceOrderService.GetAllAsync()).ToList();

            // El mes corresponde a la finalización, aunque la orden se haya creado antes.
            var completedThisMonth = orders.Where(order =>
                order.Status == ServiceOrderStatus.Completed &&
                order.CompletionDate >= monthStart &&
                order.CompletionDate < nextMonthStart).ToList();

            return new DashboardDto
            {
                MonthStart = monthStart,
                PendingOrders = orders.Count(order => order.Status == ServiceOrderStatus.Pending),
                InProgressOrders = orders.Count(order => order.Status == ServiceOrderStatus.InProgress),
                CompletedOrdersThisMonth = completedThisMonth.Count,
                MonthlyRevenue = completedThisMonth.Sum(order => order.TotalAmount),
                RecentOrders = orders.OrderByDescending(order => order.OrderDate)
                    .ThenByDescending(order => order.Id)
                    .Take(5)
                    .ToList()
            };
        }
    }
}
