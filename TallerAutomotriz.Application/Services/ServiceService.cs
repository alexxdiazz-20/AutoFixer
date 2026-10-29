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
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceDto>> GetAllAsync()
        {
            var services = await _unitOfWork.ServiceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }

        public async Task<ServiceDto> GetByIdAsync(int id)
        {
            var service = await _unitOfWork.ServiceRepository.GetByIdAsync(id);
            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<ServiceDto> CreateAsync(CreateServiceDto serviceDto)
        {
            var service = _mapper.Map<Service>(serviceDto);
            service.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.ServiceRepository.AddAsync(service);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<ServiceDto> UpdateAsync(int id, UpdateServiceDto serviceDto)
        {
            var existingService = await _unitOfWork.ServiceRepository.GetByIdAsync(id);

            if (existingService == null)
                return null;

            _mapper.Map(serviceDto, existingService);
            existingService.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.ServiceRepository.Update(existingService);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ServiceDto>(existingService);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var service = await _unitOfWork.ServiceRepository.GetByIdAsync(id);

            if (service == null)
                return false;

            // No se puede borrar un servicio que ya se usó en órdenes
            var orderCount = (await _unitOfWork.ServiceOrderDetailRepository.FindAsync(d => d.ServiceId == id))
                .Select(d => d.ServiceOrderId)
                .Distinct()
                .Count();
            if (orderCount > 0)
                throw new BusinessRuleException(
                    $"No se puede eliminar: el servicio se usó en {orderCount} {(orderCount == 1 ? "orden de servicio" : "órdenes de servicio")}.");

            _unitOfWork.ServiceRepository.Remove(service);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}