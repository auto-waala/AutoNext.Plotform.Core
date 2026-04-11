using AutoMapper;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;

namespace AutoNext.Plotform.Core.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LocationCreateDto, Location>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<Location, LocationResponseDto>();
            CreateMap<CityArea, CityAreaDto>();

            // Vehicle Type mappings
            CreateMap<VehicleType, VehicleTypeResponseDto>();
            CreateMap<VehicleTypeCreateDto, VehicleType>();
            CreateMap<VehicleTypeUpdateDto, VehicleType>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Fuel Type mappings
            CreateMap<FuelType, FuelTypeResponseDto>();
            CreateMap<FuelTypeCreateDto, FuelType>();
            CreateMap<FuelTypeUpdateDto, FuelType>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Transmission mappings
            CreateMap<Transmission, TransmissionResponseDto>();
            CreateMap<TransmissionCreateDto, Transmission>();
            CreateMap<TransmissionUpdateDto, Transmission>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
