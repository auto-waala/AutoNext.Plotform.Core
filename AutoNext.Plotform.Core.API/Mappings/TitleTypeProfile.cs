using AutoMapper;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;

namespace AutoNext.Plotform.Core.API.Mappings
{
    public class TitleTypeProfile : Profile
    {
        public TitleTypeProfile()
        {
            // Create
            CreateMap<TitleTypeCreateDto, TitleType>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.IsActive, o => o.MapFrom(_ => true));

            // Update
            CreateMap<TitleTypeUpdateDto, TitleType>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore());

            // Response
            CreateMap<TitleType, TitleTypeResponseDto>();
        }
    }
}