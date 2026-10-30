using TallerAutomotriz.Domain.Entities;
using TallerAutomotriz.Infrastructure.Data;
using TallerAutomotriz.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerAutomotriz.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Customer> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(c => c.Vehicles)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public override async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _dbSet
                .Include(c => c.Vehicles)
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> SearchAsync(string searchTerm)
        {
            var query = _dbSet.Include(c => c.Vehicles).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.Trim().ToLower();
                query = query.Where(c => 
                    c.FullName.ToLower().Contains(searchTerm) || 
                    c.Phone.Contains(searchTerm));
            }

            return await query.ToListAsync();
        }
    }
}