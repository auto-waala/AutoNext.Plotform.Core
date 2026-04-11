using AutoNext.Plotform.Core.API.Models.DTOs;

namespace AutoNext.Plotform.Core.API.Services
{
    public interface IFeatureService
    {
        Task<FeatureResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<FeatureResponseDto>> GetAllAsync();
        Task<IEnumerable<FeatureResponseDto>> GetActiveAsync();
        Task<IEnumerable<FeatureResponseDto>> GetByCategoryAsync(string category);
        Task<FeatureResponseDto> CreateAsync(FeatureCreateDto dto);
        Task<FeatureResponseDto?> UpdateAsync(Guid id, FeatureUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ToggleStatusAsync(Guid id, bool isActive);
    }
}