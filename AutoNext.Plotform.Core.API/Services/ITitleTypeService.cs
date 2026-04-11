using AutoNext.Plotform.Core.API.Models.DTOs;

namespace AutoNext.Plotform.Core.API.Services
{
    public interface ITitleTypeService
    {
        Task<IEnumerable<TitleTypeResponseDto>> GetAllAsync();
        Task<IEnumerable<TitleTypeResponseDto>> GetActiveAsync();
        Task<TitleTypeResponseDto?> GetByIdAsync(Guid id);
        Task<TitleTypeResponseDto> CreateAsync(TitleTypeCreateDto dto);
        Task<TitleTypeResponseDto?> UpdateAsync(Guid id, TitleTypeUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ToggleStatusAsync(Guid id, bool isActive);
    }
}