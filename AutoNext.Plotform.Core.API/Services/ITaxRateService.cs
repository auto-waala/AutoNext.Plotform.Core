using AutoNext.Plotform.Core.API.Models.DTOs;

namespace AutoNext.Plotform.Core.API.Services
{
    public interface ITaxRateService
    {
        Task<IEnumerable<TaxRateResponseDto>> GetAllAsync();
        Task<IEnumerable<TaxRateResponseDto>> GetActiveAsync();
        Task<TaxRateResponseDto?> GetByIdAsync(Guid id);
        Task<TaxRateResponseDto> CreateAsync(TaxRateCreateDto dto);
        Task<TaxRateResponseDto?> UpdateAsync(Guid id, TaxRateUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ToggleStatusAsync(Guid id, bool isActive);
    }
}