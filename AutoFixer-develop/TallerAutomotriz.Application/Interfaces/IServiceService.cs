using System.Collections.Generic;
using System.Threading.Tasks;
using TallerAutomotriz.Application.DTOs;

namespace TallerAutomotriz.Application.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetAllAsync();
        Task<ServiceDto> GetByIdAsync(int id);
        Task<ServiceDto> CreateAsync(CreateServiceDto serviceDto);
        Task<ServiceDto> UpdateAsync(int id, UpdateServiceDto serviceDto);
        Task<bool> DeleteAsync(int id);
    }
}