using System;
using System.Threading.Tasks;

namespace TallerAutomotriz.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository CustomerRepository { get; }
        IVehicleRepository VehicleRepository { get; }
        IServiceRepository ServiceRepository { get; }
        IMechanicRepository MechanicRepository { get; }
        IServiceOrderRepository ServiceOrderRepository { get; }
        IServiceOrderDetailRepository ServiceOrderDetailRepository { get; }

        Task<int> SaveChangesAsync();
    }
}