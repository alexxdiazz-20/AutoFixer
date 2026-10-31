using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TallerAutomotriz.Application.DTOs;
using TallerAutomotriz.Domain.Entities;

namespace TallerAutomotriz.Application.Interfaces
{
    public interface IServiceOrderService
    {
        Task<IEnumerable<ServiceOrderDto>> GetAllAsync();
        Task<IEnumerable<ServiceOrderDto>> GetFilteredAsync(ServiceOrderStatus? status, DateTime? startDate, DateTime? endDate); // NUEVO
        Task<ServiceOrderDto> GetByIdAsync(int id);
        Task<IEnumerable<ServiceOrderDto>> GetByVehicleIdAsync(int vehicleId);
        Task<IEnumerable<ServiceOrderDto>> GetByMechanicIdAsync(int mechanicId);
        Task<ServiceOrderDto> CreateAsync(CreateServiceOrderDto serviceOrderDto);
        Task<ServiceOrderDto> UpdateAsync(int id, UpdateServiceOrderDto serviceOrderDto);
        Task<bool> DeleteAsync(int id);
        Task<ServiceOrderDto> ChangeStatusAsync(int id, ServiceOrderStatus newStatus);
    }
}