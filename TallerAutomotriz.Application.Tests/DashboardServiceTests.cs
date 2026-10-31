using TallerAutomotriz.Application.DTOs;
using TallerAutomotriz.Application.Interfaces;
using TallerAutomotriz.Application.Services;
using TallerAutomotriz.Domain.Entities;

namespace TallerAutomotriz.Application.Tests;

public class DashboardServiceTests
{
    [Fact]
    public async Task Empty_workshop_has_zero_totals_and_no_recent_orders()
    {
        var result = await CreateService().GetSummaryAsync();

        Assert.Equal(0, result.PendingOrders);
        Assert.Equal(0, result.InProgressOrders);
        Assert.Equal(0, result.CompletedOrdersThisMonth);
        Assert.Equal(0m, result.MonthlyRevenue);
        Assert.Empty(result.RecentOrders);
    }

    [Fact]
    public async Task Open_orders_are_counted_across_all_months()
    {
        var result = await CreateService(
            Order(1, ServiceOrderStatus.Pending, new DateTime(2025, 1, 1)),
            Order(2, ServiceOrderStatus.Pending),
            Order(3, ServiceOrderStatus.InProgress, new DateTime(2026, 9, 15)),
            Order(4, ServiceOrderStatus.Completed),
            Order(5, ServiceOrderStatus.Cancelled)).GetSummaryAsync();

        Assert.Equal(2, result.PendingOrders);
        Assert.Equal(1, result.InProgressOrders);
    }

    [Fact]
    public async Task Revenue_uses_completion_month_and_excludes_other_statuses_and_missing_dates()
    {
        var first = new DateTime(2026, 10, 1);
        var next = first.AddMonths(1);
        var result = await CreateService(
            Order(1, ServiceOrderStatus.Completed, completion: first.AddTicks(-1), total: 900m),
            Order(2, ServiceOrderStatus.Completed, new DateTime(2026, 9, 1), first, 100m),
            Order(3, ServiceOrderStatus.Completed, completion: next.AddTicks(-1), total: 50.25m),
            Order(4, ServiceOrderStatus.Completed, completion: next, total: 800m),
            Order(5, ServiceOrderStatus.Completed, total: 700m),
            Order(6, ServiceOrderStatus.Cancelled, completion: first, total: 600m),
            Order(7, ServiceOrderStatus.InProgress, completion: first, total: 500m)).GetSummaryAsync();

        Assert.Equal(first, result.MonthStart);
        Assert.Equal(2, result.CompletedOrdersThisMonth);
        Assert.Equal(150.25m, result.MonthlyRevenue);
    }

    [Fact]
    public async Task Recent_orders_are_limited_to_five_with_deterministic_date_and_id_order()
    {
        var day = new DateTime(2026, 10, 20);
        var result = await CreateService(
            Order(2, date: day), Order(6, ServiceOrderStatus.Cancelled, day.AddDays(1)),
            Order(1, date: day.AddDays(-1)), Order(4, date: day),
            Order(7, date: day.AddDays(2)), Order(3, date: day), Order(5, date: day))
            .GetSummaryAsync();

        Assert.Equal(new[] { 7, 6, 5, 4, 3 }, result.RecentOrders.Select(order => order.Id));
    }

    [Fact]
    public async Task December_summary_excludes_orders_completed_in_the_next_year()
    {
        var clock = new FixedTimeProvider(new DateTimeOffset(2026, 12, 15, 12, 0, 0, TimeSpan.Zero));
        var service = new DashboardService(new StubOrders(new[]
        {
            Order(1, ServiceOrderStatus.Completed, completion: new DateTime(2026, 12, 31), total: 75m),
            Order(2, ServiceOrderStatus.Completed, completion: new DateTime(2027, 1, 1), total: 90m)
        }), clock);

        var result = await service.GetSummaryAsync();

        Assert.Equal(1, result.CompletedOrdersThisMonth);
        Assert.Equal(75m, result.MonthlyRevenue);
    }

    [Fact]
    public async Task Month_follows_the_local_clock_at_a_utc_month_boundary()
    {
        var zone = TimeZoneInfo.CreateCustomTimeZone("Workshop", TimeSpan.FromHours(-4), "Workshop", "Workshop");
        var clock = new FixedTimeProvider(new DateTimeOffset(2026, 11, 1, 2, 0, 0, TimeSpan.Zero), zone);
        var result = await new DashboardService(new StubOrders(Array.Empty<ServiceOrderDto>()), clock)
            .GetSummaryAsync();

        Assert.Equal(new DateTime(2026, 10, 1), result.MonthStart);
    }

    private static DashboardService CreateService(params ServiceOrderDto[] orders) =>
        new(new StubOrders(orders), new FixedTimeProvider(
            new DateTimeOffset(2026, 10, 30, 12, 0, 0, TimeSpan.Zero)));

    private static ServiceOrderDto Order(int id, ServiceOrderStatus status = ServiceOrderStatus.Pending,
        DateTime? date = null, DateTime? completion = null, decimal total = 0m) => new()
        {
            Id = id, Status = status, OrderDate = date ?? new DateTime(2026, 10, 15),
            CompletionDate = completion, TotalAmount = total
        };

    private sealed class FixedTimeProvider(DateTimeOffset now, TimeZoneInfo? zone = null) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => now;
        public override TimeZoneInfo LocalTimeZone => zone ?? TimeZoneInfo.Utc;
    }

    private sealed class StubOrders(IEnumerable<ServiceOrderDto> orders) : IServiceOrderService
    {
        public Task<IEnumerable<ServiceOrderDto>> GetAllAsync() => Task.FromResult(orders);
        public Task<ServiceOrderDto> GetByIdAsync(int id) => throw new NotSupportedException();
        public Task<IEnumerable<ServiceOrderDto>> GetByVehicleIdAsync(int id) => throw new NotSupportedException();
        public Task<IEnumerable<ServiceOrderDto>> GetByMechanicIdAsync(int id) => throw new NotSupportedException();
        public Task<ServiceOrderDto> CreateAsync(CreateServiceOrderDto dto) => throw new NotSupportedException();
        public Task<ServiceOrderDto> UpdateAsync(int id, UpdateServiceOrderDto dto) => throw new NotSupportedException();
        public Task<bool> DeleteAsync(int id) => throw new NotSupportedException();
    }
}
