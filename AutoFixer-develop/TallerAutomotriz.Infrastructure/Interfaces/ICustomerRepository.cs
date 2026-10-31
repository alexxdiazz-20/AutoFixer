using TallerAutomotriz.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TallerAutomotriz.Infrastructure.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<IEnumerable<Customer>> SearchAsync(string searchTerm);
    }
}