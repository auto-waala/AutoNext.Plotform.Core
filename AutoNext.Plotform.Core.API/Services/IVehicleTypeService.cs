using AutoNext.Plotform.Core.API.Models.DTOs;

namespace AutoNext.Plotform.Core.API.Services
{
    public interface IVehicleTypeService
    {
        Task<VehicleTypeResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<VehicleTypeResponseDto>> GetAllAsync(bool onlyActive = false);
        Task<VehicleTypeResponseDto> CreateAsync(VehicleTypeCreateDto createDto);
        Task<VehicleTypeResponseDto?> UpdateAsync(VehicleTypeUpdateDto updateDto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ToggleActiveAsync(Guid id);
    }
}
