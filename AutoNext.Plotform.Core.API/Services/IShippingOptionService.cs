using AutoNext.Plotform.Core.API.Models.DTOs;

namespace AutoNext.Plotform.Core.API.Services
{
    public interface IShippingOptionService
    {
        Task<IEnumerable<ShippingOptionResponseDto>> GetAllAsync();
        Task<IEnumerable<ShippingOptionResponseDto>> GetActiveAsync();
        Task<ShippingOptionResponseDto?> GetByIdAsync(Guid id);
        Task<ShippingOptionResponseDto> CreateAsync(ShippingOptionCreateDto dto);
        Task<ShippingOptionResponseDto?> UpdateAsync(Guid id, ShippingOptionUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ToggleStatusAsync(Guid id, bool isActive);
    }
}