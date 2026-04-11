using AutoMapper;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;
using System.Text.Json;

namespace AutoNext.Plotform.Core.API.Mappings
{
    public class PaymentMethodProfile : Profile
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public PaymentMethodProfile()
        {
            // Create
            CreateMap<PaymentMethodCreateDto, PaymentMethod>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.Metadata,
                    opt => opt.MapFrom(src =>
                        src.Metadata != null
                            ? JsonSerializer.Serialize(src.Metadata, _jsonOptions)
                            : null));

            // Update
            CreateMap<PaymentMethodUpdateDto, PaymentMethod>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.Metadata,
                    opt => opt.MapFrom(src =>
                        src.Metadata != null
                            ? JsonSerializer.Serialize(src.Metadata, _jsonOptions)
                            : null));

            // Response
            CreateMap<PaymentMethod, PaymentMethodResponseDto>()
                .ForMember(dest => dest.Metadata,
                    opt => opt.MapFrom(src =>
                        !string.IsNullOrWhiteSpace(src.Metadata)
                            ? JsonSerializer.Deserialize<Dictionary<string, object>>(src.Metadata, _jsonOptions)
                            : null));
        }
    }
}