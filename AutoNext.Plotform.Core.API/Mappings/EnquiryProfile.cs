using AutoMapper;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;

namespace AutoNext.Plotform.Core.API.Mappings
{
    public class EnquiryProfile : Profile
    {
        public EnquiryProfile()
        {
            // CreateDto -> Entity
            CreateMap<EnquiryCreateDto, Enquiry>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.Ignore())
                .ForMember(dest => dest.IsActive,
                    opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(_ => "Pending"));

            // Entity -> ResponseDto
            CreateMap<Enquiry, EnquiryResponseDto>();
        }
    }
}
