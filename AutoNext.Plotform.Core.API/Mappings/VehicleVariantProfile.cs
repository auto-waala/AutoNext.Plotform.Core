using AutoMapper;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;
using AutoNext.Plotform.Core.API.Utilitys;

namespace AutoNext.Plotform.Core.API.Mappings
{
    public class VehicleVariantProfile : Profile
    {
        public VehicleVariantProfile()
        {
            // ── CreateDto ──► Entity ──────────────────────────────────────────
            CreateMap<VehicleVariantCreateDto, VehicleVariant>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.Ignore())
                .ForMember(dest => dest.IsActive,
                    opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.Metadata,
                    opt => opt.MapFrom(src => JsonMappingHelpers.SerializeObject(src.Metadata)))
                .ForMember(dest => dest.Model,
                    opt => opt.Ignore())
                .ForMember(dest => dest.FuelType,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Transmission,
                    opt => opt.Ignore());

            // ── UpdateDto ──► Entity ──────────────────────────────────────────
            CreateMap<VehicleVariantUpdateDto, VehicleVariant>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.Ignore())
                .ForMember(dest => dest.IsActive,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Name,
                    opt => opt.Condition(src => src.Name != null))
                .ForMember(dest => dest.Code,
                    opt => opt.Condition(src => src.Code != null))
                .ForMember(dest => dest.Description,
                    opt => opt.Condition(src => src.Description != null))
                .ForMember(dest => dest.DriveType,
                    opt => opt.Condition(src => src.DriveType != null))
                .ForMember(dest => dest.EngineSize,
                    opt => opt.Condition(src => src.EngineSize.HasValue))
                .ForMember(dest => dest.Horsepower,
                    opt => opt.Condition(src => src.Horsepower.HasValue))
                .ForMember(dest => dest.Torque,
                    opt => opt.Condition(src => src.Torque.HasValue))
                .ForMember(dest => dest.SeatingCapacity,
                    opt => opt.Condition(src => src.SeatingCapacity.HasValue))
                .ForMember(dest => dest.DoorsCount,
                    opt => opt.Condition(src => src.DoorsCount.HasValue))
                .ForMember(dest => dest.BasePrice,
                    opt => opt.Condition(src => src.BasePrice.HasValue))
                .ForMember(dest => dest.IsAvailable,
                    opt => opt.Condition(src => src.IsAvailable.HasValue))
                .ForMember(dest => dest.DisplayOrder,
                    opt => opt.Condition(src => src.DisplayOrder.HasValue))
                .ForMember(dest => dest.IsActive,
                    opt => opt.Condition(src => src.IsActive.HasValue))
                .ForMember(dest => dest.Metadata,
                    opt => opt.MapFrom(src => JsonMappingHelpers.SerializeObject(src.Metadata)))
                .ForMember(dest => dest.Model,
                    opt => opt.Ignore())
                .ForMember(dest => dest.FuelType,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Transmission,
                    opt => opt.Ignore());

            // ── Entity ──► ResponseDto ────────────────────────────────────────
            CreateMap<VehicleVariant, VehicleVariantResponseDto>()
                .ForMember(dest => dest.Metadata,
                    opt => opt.MapFrom(src => JsonMappingHelpers.DeserializeDict(src.Metadata)))
                .ForMember(dest => dest.ModelName,
                    opt => opt.MapFrom(src => src.Model != null ? src.Model.Name : null))
                .ForMember(dest => dest.ModelCode,
                    opt => opt.MapFrom(src => src.Model != null ? src.Model.Code : null))
                .ForMember(dest => dest.FuelTypeName,
                    opt => opt.MapFrom(src => src.FuelType != null ? src.FuelType.Name : null))
                .ForMember(dest => dest.TransmissionName,
                    opt => opt.MapFrom(src => src.Transmission != null ? src.Transmission.Name : null));


            // ── FilterDto ──► Entity ──────────────────────────────────────────
            CreateMap<VehicleVariantFilterDto, VehicleVariant>()
                .ForAllMembers(opt => opt.Condition(
                    (src, dest, srcMember) => srcMember != null));
        }
    }
}