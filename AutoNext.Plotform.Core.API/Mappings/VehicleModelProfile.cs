using AutoMapper;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;
using System.Text.Json;

namespace AutoNext.Plotform.Core.API.Mappings
{
    public class VehicleModelProfile : Profile
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public VehicleModelProfile()
        {
            // ── CreateDto → Entity ───────────────────────────────
            CreateMap<VehicleModelCreateDto, VehicleModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.Metadata,
                    opt => opt.MapFrom(src =>
                        src.Metadata != null
                            ? JsonSerializer.Serialize(src.Metadata, _jsonOptions)
                            : null));

            // ── UpdateDto → Entity ───────────────────────────────
            CreateMap<VehicleModelUpdateDto, VehicleModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Metadata,
                    opt => opt.MapFrom(src =>
                        src.Metadata != null
                            ? JsonSerializer.Serialize(src.Metadata, _jsonOptions)
                            : null));

            // ── Entity → ResponseDto ─────────────────────────────
            CreateMap<VehicleModel, VehicleModelResponseDto>()
                .ForMember(dest => dest.Metadata,
                    opt => opt.MapFrom(src =>
                        !string.IsNullOrWhiteSpace(src.Metadata)
                            ? JsonSerializer.Deserialize<Dictionary<string, object>>(src.Metadata, _jsonOptions)
                            : null))

                // 🔥 Optional mapping (if navigation loaded)
                .ForMember(dest => dest.BrandName,
                    opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : null))

                .ForMember(dest => dest.VehicleTypeName,
                    opt => opt.MapFrom(src => src.VehicleType != null ? src.VehicleType.Name : null));
        }
    }
}