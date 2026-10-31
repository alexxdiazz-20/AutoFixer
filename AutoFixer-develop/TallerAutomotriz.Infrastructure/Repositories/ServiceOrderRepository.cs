using TallerAutomotriz.Domain.Entities;
using TallerAutomotriz.Infrastructure.Data;
using TallerAutomotriz.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerAutomotriz.Infrastructure.Repositories
{
    public class ServiceOrderRepository : BaseRepository<ServiceOrder>, IServiceOrderRepository
    {
        public ServiceOrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<ServiceOrder> GetByIdAsync(int id)
        {
            return await _dbSet
                .Where(so => so.Id == id)
                .Include(so => so.Vehicle)
                .Include(so => so.Mechanic)
                .Include(so => so.Details)
                    .ThenInclude(d => d.Service)
                .FirstOrDefaultAsync();
        }

        public override async Task<IEnumerable<ServiceOrder>> GetAllAsync()
        {
            return await _dbSet
                .Include(so => so.Vehicle)
                .Include(so => so.Mechanic)
                .Include(so => so.Details)
                    .ThenInclude(d => d.Service)
                .ToListAsync();
        }

        public async Task<IEnumerable<ServiceOrder>> GetByVehicleIdAsync(int vehicleId)
        {
            return await _dbSet
                .Where(so => so.VehicleId == vehicleId)
                .Include(so => so.Vehicle)
                .Include(so => so.Mechanic)
                .Include(so => so.Details)
                    .ThenInclude(d => d.Service)
                .ToListAsync();
        }

        public async Task<IEnumerable<ServiceOrder>> GetByMechanicIdAsync(int mechanicId)
        {
            return await _dbSet
                .Where(so => so.MechanicId == mechanicId)
                .Include(so => so.Vehicle)
                .Include(so => so.Mechanic)
                .Include(so => so.Details)
                    .ThenInclude(d => d.Service)
                .ToListAsync();
        }
    }
public async Task<IEnumerable<ServiceOrder>> GetFilteredAsync(ServiceOrderStatus? status, DateTime? startDate, DateTime? endDate)
{
    var query = _dbSet
        .Include(o => o.Vehicle)
        .Include(o => o.Mechanic)
        .Include(o => o.Details)
        .AsQueryable();

    if (status.HasValue)
    {
        query = query.Where(o => o.Status == status.Value);
    }

    if (startDate.HasValue)
    {
        query = query.Where(o => o.OrderDate >= startDate.Value);
    }

    if (endDate.HasValue)
    {
        var end = endDate.Value.Date.AddDays(1).AddTicks(-1);
        query = query.Where(o => o.OrderDate <= end);
    }

    return await query.ToListAsync();
}
}
