using AutoMapper;
using TallerAutomotriz.Application.DTOs;
using TallerAutomotriz.Domain.Entities;

namespace TallerAutomotriz.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeos de Customer
            CreateMap<Customer, CustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>();

            // Mapeos de Vehicle
            CreateMap<Vehicle, VehicleDto>()
                .ForMember(dest => dest.CustomerName,
                    opt => opt.MapFrom(src => $"{src.Customer.Name} {src.Customer.LastName}"));
            CreateMap<CreateVehicleDto, Vehicle>();
            CreateMap<UpdateVehicleDto, Vehicle>();

            // Mapeos de Service
            CreateMap<Service, ServiceDto>();
            CreateMap<CreateServiceDto, Service>();
            CreateMap<UpdateServiceDto, Service>();

            // Mapeos de Mechanic
            CreateMap<Mechanic, MechanicDto>();
            CreateMap<CreateMechanicDto, Mechanic>();
            CreateMap<UpdateMechanicDto, Mechanic>();

            // Mapeos de ServiceOrder
            CreateMap<ServiceOrder, ServiceOrderDto>()
                .ForMember(dest => dest.VehicleLicensePlate,
                    opt => opt.MapFrom(src => src.Vehicle.LicensePlate))
                .ForMember(dest => dest.VehicleBrandModel,
                    opt => opt.MapFrom(src => $"{src.Vehicle.Brand} {src.Vehicle.Model}"))
                .ForMember(dest => dest.MechanicFullName,
                    opt => opt.MapFrom(src => $"{src.Mechanic.Name} {src.Mechanic.LastName}"));
            // Los detalles se arman a mano en ServiceOrderService (precio real y total)
            CreateMap<CreateServiceOrderDto, ServiceOrder>()
                .ForMember(dest => dest.Details, opt => opt.Ignore());
            CreateMap<UpdateServiceOrderDto, ServiceOrder>()
                .ForMember(dest => dest.Details, opt => opt.Ignore());

            // Mapeos de ServiceOrderDetail
            CreateMap<ServiceOrderDetail, ServiceOrderDetailDto>()
                .ForMember(dest => dest.ServiceName,
                    opt => opt.MapFrom(src => src.Service.Name));
            CreateMap<CreateServiceOrderDetailDto, ServiceOrderDetail>();
        }
    }
}