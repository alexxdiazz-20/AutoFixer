using System.Collections.Generic;
using System.Threading.Tasks;
using TallerAutomotriz.Application.DTOs;

namespace TallerAutomotriz.Application.Interfaces
{
    public interface IServiceOrderService
    {
        Task<IEnumerable<ServiceOrderDto>> GetAllAsync();
        Task<ServiceOrderDto> GetByIdAsync(int id);
        Task<IEnumerable<ServiceOrderDto>> GetByVehicleIdAsync(int vehicleId);
        Task<IEnumerable<ServiceOrderDto>> GetByMechanicIdAsync(int mechanicId);
        Task<ServiceOrderDto> CreateAsync(CreateServiceOrderDto serviceOrderDto);
        Task<ServiceOrderDto?> UpdateAsync(int id, UpdateServiceOrderDto serviceOrderDto);
        Task<bool> DeleteAsync(int id);
    }
}