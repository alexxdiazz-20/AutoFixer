using TallerAutomotriz.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TallerAutomotriz.Infrastructure.Interfaces
{
    public interface IServiceOrderRepository : IBaseRepository<ServiceOrder>
    {
        Task<IEnumerable<ServiceOrder>> GetByVehicleIdAsync(int vehicleId);
        Task<IEnumerable<ServiceOrder>> GetByMechanicIdAsync(int mechanicId);
        Task<IEnumerable<ServiceOrder>> GetFilteredAsync(ServiceOrderStatus? status, DateTime? startDate, DateTime? endDate);
    }
}