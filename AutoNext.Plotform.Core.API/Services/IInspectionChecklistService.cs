using AutoNext.Plotform.Core.API.Models.DTOs;

namespace AutoNext.Plotform.Core.API.Services
{
    public interface IInspectionChecklistService
    {
        Task<InspectionChecklistResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<InspectionChecklistResponseDto>> GetAllAsync();
        Task<IEnumerable<InspectionChecklistResponseDto>> GetActiveAsync();
        Task<IEnumerable<InspectionChecklistResponseDto>> GetByCategoryAsync(string category);
        Task<InspectionChecklistResponseDto> CreateAsync(InspectionChecklistCreateDto dto);
        Task<InspectionChecklistResponseDto?> UpdateAsync(Guid id, InspectionChecklistUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ToggleStatusAsync(Guid id, bool isActive);
    }
}