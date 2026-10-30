using TallerAutomotriz.Infrastructure.Data;
using TallerAutomotriz.Infrastructure.Interfaces;
using TallerAutomotriz.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace TallerAutomotriz.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ICustomerRepository _customerRepository;
        private IVehicleRepository _vehicleRepository;
        private IServiceRepository _serviceRepository;
        private IMechanicRepository _mechanicRepository;
        private IServiceOrderRepository _serviceOrderRepository;
        private IServiceOrderDetailRepository _serviceOrderDetailRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICustomerRepository CustomerRepository =>
            _customerRepository ??= new CustomerRepository(_context);

        public IVehicleRepository VehicleRepository =>
            _vehicleRepository ??= new VehicleRepository(_context);

        public IServiceRepository ServiceRepository =>
            _serviceRepository ??= new ServiceRepository(_context);

        public IMechanicRepository MechanicRepository =>
            _mechanicRepository ??= new MechanicRepository(_context);

        public IServiceOrderRepository ServiceOrderRepository =>
            _serviceOrderRepository ??= new ServiceOrderRepository(_context);

        public IServiceOrderDetailRepository ServiceOrderDetailRepository =>
            _serviceOrderDetailRepository ??= new ServiceOrderDetailRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}