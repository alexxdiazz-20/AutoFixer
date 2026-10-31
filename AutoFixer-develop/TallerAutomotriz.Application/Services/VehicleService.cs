using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TallerAutomotriz.Application.DTOs;
using TallerAutomotriz.Application.Exceptions;
using TallerAutomotriz.Application.Interfaces;
using TallerAutomotriz.Domain.Entities;
using TallerAutomotriz.Infrastructure.Interfaces;

namespace TallerAutomotriz.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VehicleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VehicleDto>> GetAllAsync()
        {
            var vehicles = await _unitOfWork.VehicleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }

        public async Task<VehicleDto> GetByIdAsync(int id)
        {
            var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id);
            return _mapper.Map<VehicleDto>(vehicle);
        }

        public async Task<IEnumerable<VehicleDto>> GetByCustomerIdAsync(int customerId)
        {
            var vehicles = await _unitOfWork.VehicleRepository.GetByCustomerIdAsync(customerId);
            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }

        public async Task<VehicleDto> CreateAsync(CreateVehicleDto vehicleDto)
        {
            var vehicle = _mapper.Map<Vehicle>(vehicleDto);
            vehicle.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.VehicleRepository.AddAsync(vehicle);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<VehicleDto>(vehicle);
        }

        public async Task<VehicleDto> UpdateAsync(int id, UpdateVehicleDto vehicleDto)
        {
            var existingVehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id);

            if (existingVehicle == null)
                return null;

            _mapper.Map(vehicleDto, existingVehicle);
            existingVehicle.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.VehicleRepository.Update(existingVehicle);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<VehicleDto>(existingVehicle);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id);

            if (vehicle == null)
                return false;

            // No se puede borrar un vehículo con historial de órdenes
            var orderCount = (await _unitOfWork.ServiceOrderRepository.FindAsync(o => o.VehicleId == id)).Count();
            if (orderCount > 0)
                throw new BusinessRuleException(
                    $"No se puede eliminar: el vehículo tiene {orderCount} {(orderCount == 1 ? "orden de servicio registrada" : "órdenes de servicio registradas")}.");

            _unitOfWork.VehicleRepository.Remove(vehicle);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
                public async Task<IEnumerable<VehicleDto>> SearchByLicensePlateAsync(string licensePlate)
        {
            var vehicles = await _unitOfWork.VehicleRepository.SearchByLicensePlateAsync(licensePlate);
            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }
    }
}