using System.Collections.Generic;
using System.Threading.Tasks;
using TallerAutomotriz.Application.DTOs;

namespace TallerAutomotriz.Application.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDto>> GetAllAsync();
        Task<IEnumerable<VehicleDto>> SearchByLicensePlateAsync(string licensePlate); // NUEVO
        Task<VehicleDto> GetByIdAsync(int id);
        Task<IEnumerable<VehicleDto>> GetByCustomerIdAsync(int customerId);
        Task<VehicleDto> CreateAsync(CreateVehicleDto vehicleDto);
        Task<VehicleDto> UpdateAsync(int id, UpdateVehicleDto vehicleDto);
        Task<bool> DeleteAsync(int id);
    }
}