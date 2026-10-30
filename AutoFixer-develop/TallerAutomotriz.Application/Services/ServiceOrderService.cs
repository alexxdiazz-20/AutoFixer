using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TallerAutomotriz.Application.DTOs;
using TallerAutomotriz.Application.Exceptions;
using TallerAutomotriz.Application.Interfaces;
using TallerAutomotriz.Domain.Entities;
using TallerAutomotriz.Infrastructure.Interfaces;

namespace TallerAutomotriz.Application.Services
{
    public class ServiceOrderService : IServiceOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceOrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceOrderDto>> GetAllAsync()
        {
            var serviceOrders = await _unitOfWork.ServiceOrderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceOrderDto>>(serviceOrders);
        }

        public async Task<ServiceOrderDto> GetByIdAsync(int id)
        {
            var serviceOrder = await _unitOfWork.ServiceOrderRepository.GetByIdAsync(id);
            return _mapper.Map<ServiceOrderDto>(serviceOrder);
        }

        public async Task<IEnumerable<ServiceOrderDto>> GetByVehicleIdAsync(int vehicleId)
        {
            var serviceOrders = await _unitOfWork.ServiceOrderRepository.GetByVehicleIdAsync(vehicleId);
            return _mapper.Map<IEnumerable<ServiceOrderDto>>(serviceOrders);
        }

        public async Task<IEnumerable<ServiceOrderDto>> GetByMechanicIdAsync(int mechanicId)
        {
            var serviceOrders = await _unitOfWork.ServiceOrderRepository.GetByMechanicIdAsync(mechanicId);
            return _mapper.Map<IEnumerable<ServiceOrderDto>>(serviceOrders);
        }

        public async Task<ServiceOrderDto> CreateAsync(CreateServiceOrderDto serviceOrderDto)
        {
            var details = await BuildDetailsAsync(serviceOrderDto.Details);

            var serviceOrder = _mapper.Map<ServiceOrder>(serviceOrderDto);
            serviceOrder.CreatedAt = DateTime.UtcNow;
            serviceOrder.Status = ServiceOrderStatus.Pending;
            serviceOrder.Details = details;
            serviceOrder.TotalAmount = details.Sum(d => d.Subtotal);

            await _unitOfWork.ServiceOrderRepository.AddAsync(serviceOrder);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(serviceOrder.Id);
        }

        public async Task<ServiceOrderDto> UpdateAsync(int id, UpdateServiceOrderDto serviceOrderDto)
        {
            var existingServiceOrder = await _unitOfWork.ServiceOrderRepository.GetByIdAsync(id);

            if (existingServiceOrder == null)
                return null;

            // NUEVA VALIDACIÓN: No se puede editar si está Completada o Cancelada
            if (existingServiceOrder.Status == ServiceOrderStatus.Completed || 
                existingServiceOrder.Status == ServiceOrderStatus.Cancelled)
            {
                throw new BusinessRuleException("No se puede editar una orden que ya ha sido Completada o Cancelada.");
            }

            var newDetails = await BuildDetailsAsync(serviceOrderDto.Details);

            _mapper.Map(serviceOrderDto, existingServiceOrder);
            existingServiceOrder.UpdatedAt = DateTime.UtcNow;

            foreach (var oldDetail in existingServiceOrder.Details.ToList())
            {
                _unitOfWork.ServiceOrderDetailRepository.Remove(oldDetail);
            }
            existingServiceOrder.Details.Clear();

            foreach (var detail in newDetails)
            {
                existingServiceOrder.Details.Add(detail);
            }
            existingServiceOrder.TotalAmount = newDetails.Sum(d => d.Subtotal);

            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        // NUEVO MÉTODO PARA EL FLUJO DE ESTADOS
        public async Task<ServiceOrderDto> ChangeStatusAsync(int id, ServiceOrderStatus newStatus)
        {
            var serviceOrder = await _unitOfWork.ServiceOrderRepository.GetByIdAsync(id);

            if (serviceOrder == null)
                return null;

            var currentStatus = serviceOrder.Status;

            // 1. Validar que no esté ya cerrada
            if (currentStatus == ServiceOrderStatus.Completed || currentStatus == ServiceOrderStatus.Cancelled)
            {
                throw new BusinessRuleException("Una orden Completada o Cancelada no puede cambiar de estado.");
            }

            // 2. Validar transiciones permitidas
            bool isValidTransition = false;

            if (currentStatus == ServiceOrderStatus.Pending)
            {
                if (newStatus == ServiceOrderStatus.InProgress || newStatus == ServiceOrderStatus.Cancelled)
                    isValidTransition = true;
            }
            else if (currentStatus == ServiceOrderStatus.InProgress)
            {
                if (newStatus == ServiceOrderStatus.Completed || newStatus == ServiceOrderStatus.Cancelled)
                    isValidTransition = true;
            }

            if (!isValidTransition)
            {
                throw new BusinessRuleException($"Transición inválida de {currentStatus} a {newStatus}.");
            }

            // 3. Aplicar el cambio y guardar la fecha si se completa
            serviceOrder.Status = newStatus;
            serviceOrder.UpdatedAt = DateTime.UtcNow;

            if (newStatus == ServiceOrderStatus.Completed)
            {
                serviceOrder.CompletionDate = DateTime.UtcNow;
            }

            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var serviceOrder = await _unitOfWork.ServiceOrderRepository.GetByIdAsync(id);

            if (serviceOrder == null)
                return false;

            _unitOfWork.ServiceOrderRepository.Remove(serviceOrder);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        private async Task<List<ServiceOrderDetail>> BuildDetailsAsync(List<CreateServiceOrderDetailDto> detailDtos)
        {
            if (detailDtos == null || detailDtos.Count == 0)
                throw new BusinessRuleException("Agrega al menos un servicio a la orden.");

            var details = new List<ServiceOrderDetail>();

            foreach (var detailDto in detailDtos)
            {
                if (detailDto.Quantity < 1)
                    throw new BusinessRuleException("La cantidad de cada servicio debe ser 1 o más.");

                var service = await _unitOfWork.ServiceRepository.GetByIdAsync(detailDto.ServiceId);
                if (service == null)
                    throw new BusinessRuleException($"El servicio seleccionado (ID {detailDto.ServiceId}) no existe.");

                details.Add(new ServiceOrderDetail
                {
                    ServiceId = service.Id,
                    Quantity = detailDto.Quantity,
                    UnitPrice = service.Price,
                    Subtotal = service.Price * detailDto.Quantity,
                    Notes = detailDto.Notes?.Trim() ?? string.Empty,
                    CreatedAt = DateTime.UtcNow
                });
            }

            return details;
        }
                public async Task<IEnumerable<ServiceOrderDto>> GetFilteredAsync(ServiceOrderStatus? status, DateTime? startDate, DateTime? endDate)
        {
            var serviceOrders = await _unitOfWork.ServiceOrderRepository.GetFilteredAsync(status, startDate, endDate);
            return _mapper.Map<IEnumerable<ServiceOrderDto>>(serviceOrders);
        }
    }
}