using System.Collections.Generic;
using System.Threading.Tasks;
using TallerAutomotriz.Application.DTOs;

namespace TallerAutomotriz.Application.Interfaces
{
    public interface IMechanicService
    {
        Task<IEnumerable<MechanicDto>> GetAllAsync();
        Task<MechanicDto> GetByIdAsync(int id);
        Task<MechanicDto> CreateAsync(CreateMechanicDto mechanicDto);
        Task<MechanicDto> UpdateAsync(int id, UpdateMechanicDto mechanicDto);
        Task<bool> DeleteAsync(int id);
    }
}