using TallerAutomotriz.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TallerAutomotriz.Infrastructure.Interfaces
{
    public interface IServiceOrderDetailRepository : IBaseRepository<ServiceOrderDetail>
    {
        Task<IEnumerable<ServiceOrderDetail>> GetByServiceOrderIdAsync(int serviceOrderId);
    }
}