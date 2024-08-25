using AutoMapper;
using Entities;

namespace Cars.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Vehicles, opt => opt.MapFrom(src => src.Vehicles));

            CreateMap<Vehicle, VehicleDto>();

            CreateMap<CreateEmployeeDto, Employee>();

            CreateMap<CreateVehicleDto, Vehicle>();
            
            CreateMap<UpdateEmployeeDto, Employee>();

            CreateMap<UpdateVehicleDto, Vehicle>();
        }
    }
}
