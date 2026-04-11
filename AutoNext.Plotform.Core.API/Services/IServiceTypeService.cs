using AutoNext.Plotform.Core.API.Models.DTOs;

namespace AutoNext.Plotform.Core.API.Services
{
    public interface IServiceTypeService
    {
        Task<IEnumerable<ServiceTypeResponseDto>> GetAllAsync();

        Task<IEnumerable<ServiceTypeResponseDto>> GetActiveAsync();

        Task<ServiceTypeResponseDto?> GetByIdAsync(Guid id);

        Task<ServiceTypeResponseDto> CreateAsync(ServiceTypeCreateDto dto);

        Task<ServiceTypeResponseDto?> UpdateAsync(Guid id, ServiceTypeUpdateDto dto);

        Task<bool> DeleteAsync(Guid id);

        Task<bool> ToggleStatusAsync(Guid id, bool isActive);
    }
}