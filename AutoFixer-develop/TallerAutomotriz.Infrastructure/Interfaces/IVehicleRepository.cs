using TallerAutomotriz.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TallerAutomotriz.Infrastructure.Interfaces
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        Task<IEnumerable<Vehicle>> GetByCustomerIdAsync(int customerId);
        
        Task<IEnumerable<Vehicle>> SearchByLicensePlateAsync(string licensePlate);
    }
}