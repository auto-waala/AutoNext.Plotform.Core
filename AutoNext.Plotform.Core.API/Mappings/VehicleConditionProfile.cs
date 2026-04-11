using AutoMapper;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;

namespace AutoNext.Plotform.Core.API.Mappings
{
    public class VehicleConditionProfile : Profile
    {
        public VehicleConditionProfile()
        {
            // Create
            CreateMap<VehicleConditionCreateDto, VehicleCondition>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.IsActive, o => o.MapFrom(_ => true));

            // Update
            CreateMap<VehicleConditionUpdateDto, VehicleCondition>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore());

            // Response
            CreateMap<VehicleCondition, VehicleConditionResponseDto>();
        }
    }
}