using TallerAutomotriz.Domain.Entities;
using TallerAutomotriz.Infrastructure.Data;
using TallerAutomotriz.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TallerAutomotriz.Infrastructure.Repositories
{
    public class MechanicRepository : BaseRepository<Mechanic>, IMechanicRepository
    {
        public MechanicRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Mechanic> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(m => m.ServiceOrders)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public override async Task<IEnumerable<Mechanic>> GetAllAsync()
        {
            return await _dbSet
                .Include(m => m.ServiceOrders)
                .ToListAsync();
        }
    }
}