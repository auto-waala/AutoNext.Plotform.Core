using AutoNext.Plotform.Core.API.Models.DTOs;

namespace AutoNext.Plotform.Core.API.Services
{
    public interface IEnquiryService
    {
        Task<IEnumerable<EnquiryResponseDto>> GetAllAsync();

        Task<EnquiryResponseDto?> GetByIdAsync(Guid id);

        Task<EnquiryResponseDto> CreateAsync(EnquiryCreateDto enquiryCreateDto);
    }
}
