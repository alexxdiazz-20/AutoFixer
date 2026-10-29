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
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> CreateAsync(CreateCustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            customer.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.CustomerRepository.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> UpdateAsync(int id, UpdateCustomerDto customerDto)
        {
            var existingCustomer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);

            if (existingCustomer == null)
                return null;

            _mapper.Map(customerDto, existingCustomer);
            existingCustomer.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.CustomerRepository.Update(existingCustomer);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CustomerDto>(existingCustomer);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);

            if (customer == null)
                return false;

            // No se puede borrar un cliente con vehículos registrados
            var vehicleCount = (await _unitOfWork.VehicleRepository.FindAsync(v => v.CustomerId == id)).Count();
            if (vehicleCount > 0)
                throw new BusinessRuleException(
                    $"No se puede eliminar: el cliente tiene {vehicleCount} {(vehicleCount == 1 ? "vehículo registrado" : "vehículos registrados")}. Elimina o reasigna sus vehículos primero.");

            _unitOfWork.CustomerRepository.Remove(customer);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}