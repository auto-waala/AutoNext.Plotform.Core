using AutoMapper;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;
using System.Text.Json;

namespace AutoNext.Plotform.Core.API.Mappings
{
    public class TaxRateProfile : Profile
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public TaxRateProfile()
        {
            // Create
            CreateMap<TaxRateCreateDto, TaxRate>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.IsActive, o => o.MapFrom(_ => true))
                .ForMember(d => d.AppliesToVehicleTypes,
                    o => o.MapFrom(s => s.AppliesToVehicleTypes != null
                        ? JsonSerializer.Serialize(s.AppliesToVehicleTypes, _jsonOptions)
                        : null));

            // Update
            CreateMap<TaxRateUpdateDto, TaxRate>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.AppliesToVehicleTypes,
                    o => o.MapFrom(s => s.AppliesToVehicleTypes != null
                        ? JsonSerializer.Serialize(s.AppliesToVehicleTypes, _jsonOptions)
                        : null));

            // Response
            CreateMap<TaxRate, TaxRateResponseDto>()
                .ForMember(d => d.AppliesToVehicleTypes,
                    o => o.MapFrom(s =>
                        !string.IsNullOrWhiteSpace(s.AppliesToVehicleTypes)
                            ? JsonSerializer.Deserialize<List<string>>(s.AppliesToVehicleTypes, _jsonOptions)
                            : null));
        }
    }
}