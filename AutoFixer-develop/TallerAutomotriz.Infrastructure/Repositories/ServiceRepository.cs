using TallerAutomotriz.Domain.Entities;
using TallerAutomotriz.Infrastructure.Data;
using TallerAutomotriz.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TallerAutomotriz.Infrastructure.Repositories
{
    public class ServiceRepository : BaseRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Service> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(s => s.ServiceOrderDetails)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public override async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _dbSet
                .Include(s => s.ServiceOrderDetails)
                .ToListAsync();
        }
    }
}