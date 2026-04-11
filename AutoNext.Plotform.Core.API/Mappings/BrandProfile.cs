using AutoMapper;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;
using System.Text.Json;

namespace AutoNext.Plotform.Core.API.Mappings
{
    public class BrandProfile : Profile
    {
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public BrandProfile()
        {
            // ── CreateDto ──► Entity ──────────────────────────────────────────
            CreateMap<BrandCreateDto, Brand>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.Ignore())
                .ForMember(dest => dest.IsActive,
                    opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.ApplicableCategories,
                    opt => opt.MapFrom(src =>
                        src.ApplicableCategories != null
                            ? JsonSerializer.Serialize(src.ApplicableCategories, _jsonOptions)
                            : null))
                .ForMember(dest => dest.Metadata,
                    opt => opt.MapFrom(src =>
                        src.Metadata != null
                            ? JsonSerializer.Serialize(src.Metadata, _jsonOptions)
                            : null));

            // ── UpdateDto ──► Entity ──────────────────────────────────────────
            CreateMap<BrandUpdateDto, Brand>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.Ignore())
                .ForMember(dest => dest.IsActive,
                    opt => opt.Ignore())
                .ForMember(dest => dest.ApplicableCategories,
                    opt => opt.MapFrom(src =>
                        src.ApplicableCategories != null
                            ? JsonSerializer.Serialize(src.ApplicableCategories, _jsonOptions)
                            : null))
                .ForMember(dest => dest.Metadata,
                    opt => opt.MapFrom(src =>
                        src.Metadata != null
                            ? JsonSerializer.Serialize(src.Metadata, _jsonOptions)
                            : null));

            // ── Entity ──► ResponseDto ────────────────────────────────────────
            CreateMap<Brand, BrandResponseDto>()
                .ForMember(dest => dest.ApplicableCategories,
                    opt => opt.MapFrom(src =>
                        !string.IsNullOrWhiteSpace(src.ApplicableCategories)
                            ? JsonSerializer.Deserialize<List<string>>(src.ApplicableCategories, _jsonOptions)
                            : null))
                .ForMember(dest => dest.Metadata,
                    opt => opt.MapFrom(src =>
                        !string.IsNullOrWhiteSpace(src.Metadata)
                            ? JsonSerializer.Deserialize<Dictionary<string, object>>(src.Metadata, _jsonOptions)
                            : null));

        }
    }
}