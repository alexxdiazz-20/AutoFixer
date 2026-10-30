using TallerAutomotriz.Domain.Entities;
using TallerAutomotriz.Infrastructure.Data;
using TallerAutomotriz.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerAutomotriz.Infrastructure.Repositories
{
    public class ServiceOrderDetailRepository : BaseRepository<ServiceOrderDetail>, IServiceOrderDetailRepository
    {
        public ServiceOrderDetailRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<ServiceOrderDetail> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(sod => sod.Service)
                .Include(sod => sod.ServiceOrder)
                .FirstOrDefaultAsync(sod => sod.Id == id);
        }

        public override async Task<IEnumerable<ServiceOrderDetail>> GetAllAsync()
        {
            return await _dbSet
                .Include(sod => sod.Service)
                .Include(sod => sod.ServiceOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<ServiceOrderDetail>> GetByServiceOrderIdAsync(int serviceOrderId)
        {
            return await _dbSet
                .Where(sod => sod.ServiceOrderId == serviceOrderId)
                .Include(sod => sod.Service)
                .ToListAsync();
        }
    }
}