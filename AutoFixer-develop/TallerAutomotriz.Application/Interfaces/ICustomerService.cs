using System.Collections.Generic;
using System.Threading.Tasks;
using TallerAutomotriz.Application.DTOs;

namespace TallerAutomotriz.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<IEnumerable<CustomerDto>> SearchAsync(string searchTerm); // NUEVO
        Task<CustomerDto> GetByIdAsync(int id);
        Task<CustomerDto> CreateAsync(CreateCustomerDto customerDto);
        Task<CustomerDto> UpdateAsync(int id, UpdateCustomerDto customerDto);
        Task<bool> DeleteAsync(int id);
    }
}