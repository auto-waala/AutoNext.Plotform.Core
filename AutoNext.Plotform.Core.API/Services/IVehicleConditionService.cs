using AutoNext.Plotform.Core.API.Models.DTOs;

namespace AutoNext.Plotform.Core.API.Services
{
    public interface IVehicleConditionService
    {
        Task<IEnumerable<VehicleConditionResponseDto>> GetAllAsync();
        Task<IEnumerable<VehicleConditionResponseDto>> GetActiveAsync();
        Task<VehicleConditionResponseDto?> GetByIdAsync(Guid id);
        Task<VehicleConditionResponseDto> CreateAsync(VehicleConditionCreateDto dto);
        Task<VehicleConditionResponseDto?> UpdateAsync(Guid id, VehicleConditionUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ToggleStatusAsync(Guid id, bool isActive);
    }
}