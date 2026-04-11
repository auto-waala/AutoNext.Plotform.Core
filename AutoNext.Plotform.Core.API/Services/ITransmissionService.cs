using AutoNext.Plotform.Core.API.Models.DTOs;

namespace AutoNext.Plotform.Core.API.Services
{
    public interface ITransmissionService
    {
        Task<TransmissionResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<TransmissionResponseDto>> GetAllAsync(bool onlyActive = false);
        Task<TransmissionResponseDto> CreateAsync(TransmissionCreateDto createDto);
        Task<TransmissionResponseDto?> UpdateAsync(TransmissionUpdateDto updateDto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ToggleActiveAsync(Guid id);
        Task<IEnumerable<TransmissionResponseDto>> GetByGearsCountAsync(int gearsCount);
    }
}
