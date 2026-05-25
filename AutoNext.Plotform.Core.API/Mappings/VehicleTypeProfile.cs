using AutoMapper;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;
using System.Text.Json;

namespace AutoNext.Plotform.Core.API.Mappings
{
    public class VehicleTypeProfile : Profile
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public VehicleTypeProfile()
        {
            // ── CreateDto ──► Entity ──────────────────────────────────────────
            CreateMap<VehicleTypeCreateDto, VehicleType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Metadata,
                    opt => opt.MapFrom(src =>
                        src.Metadata != null
                            ? JsonSerializer.Serialize(src.Metadata, _jsonOptions)
                            : null));

            // ── UpdateDto ──► Entity ──────────────────────────────────────────
            CreateMap<VehicleTypeUpdateDto, VehicleType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Metadata,
                    opt => opt.MapFrom(src =>
                        src.Metadata != null
                            ? JsonSerializer.Serialize(src.Metadata, _jsonOptions)
                            : null));

            // ── Entity ──► ResponseDto ────────────────────────────────────────
            CreateMap<VehicleType, VehicleTypeResponseDto>()
                .ForMember(dest => dest.Metadata,
                    opt => opt.MapFrom(src =>
                        !string.IsNullOrWhiteSpace(src.Metadata)
                            ? JsonSerializer.Deserialize<Dictionary<string, object>>(src.Metadata, _jsonOptions)
                            : null));
        }
    }
}
