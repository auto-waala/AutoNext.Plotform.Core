using AutoNext.Plotform.Core.API.Models.DTOs;

namespace AutoNext.Plotform.Core.API.Services
{
    public interface IVehicleModelService
    {
        Task<VehicleModelResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<VehicleModelResponseDto>> GetAllAsync();
        Task<IEnumerable<VehicleModelResponseDto>> GetByBrandAsync(Guid brandId);
        Task<VehicleModelResponseDto> CreateAsync(VehicleModelCreateDto dto);
        Task<VehicleModelResponseDto?> UpdateAsync(Guid id, VehicleModelUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
