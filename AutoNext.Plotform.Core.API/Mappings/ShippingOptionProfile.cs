using AutoMapper;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;
using System.Text.Json;

namespace AutoNext.Plotform.Core.API.Mappings
{
    public class ShippingOptionProfile : Profile
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ShippingOptionProfile()
        {
            // Create
            CreateMap<ShippingOptionCreateDto, ShippingOption>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.IsActive, o => o.MapFrom(_ => true))
                .ForMember(d => d.ApplicableVehicleTypes,
                    o => o.MapFrom(s => s.ApplicableVehicleTypes != null
                        ? JsonSerializer.Serialize(s.ApplicableVehicleTypes, _jsonOptions)
                        : null));

            // Update
            CreateMap<ShippingOptionUpdateDto, ShippingOption>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.ApplicableVehicleTypes,
                    o => o.MapFrom(s => s.ApplicableVehicleTypes != null
                        ? JsonSerializer.Serialize(s.ApplicableVehicleTypes, _jsonOptions)
                        : null));

            // Response
            CreateMap<ShippingOption, ShippingOptionResponseDto>()
                .ForMember(d => d.ApplicableVehicleTypes,
                    o => o.MapFrom(s =>
                        !string.IsNullOrWhiteSpace(s.ApplicableVehicleTypes)
                            ? JsonSerializer.Deserialize<List<string>>(s.ApplicableVehicleTypes, _jsonOptions)
                            : null));
        }
    }
}