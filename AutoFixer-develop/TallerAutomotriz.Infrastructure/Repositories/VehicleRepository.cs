using TallerAutomotriz.Domain.Entities;
using TallerAutomotriz.Infrastructure.Data;
using TallerAutomotriz.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerAutomotriz.Infrastructure.Repositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Vehicle> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(v => v.Customer)
                .Include(v => v.ServiceOrders)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public override async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await _dbSet
                .Include(v => v.Customer)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetByCustomerIdAsync(int customerId)
        {
            return await _dbSet
                .Where(v => v.CustomerId == customerId)
                .Include(v => v.Customer)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> SearchByLicensePlateAsync(string licensePlate)
        {
            var query = _dbSet.Include(v => v.Customer).AsQueryable();

            if (!string.IsNullOrWhiteSpace(licensePlate))
            {
                licensePlate = licensePlate.Trim().ToLower();
                query = query.Where(v => v.LicensePlate.ToLower().Contains(licensePlate));
            }

            return await query.ToListAsync();
        }
    }
}