using AutoNext.Plotform.Core.API.Models.DTOs;

namespace AutoNext.Plotform.Core.API.Services
{
    public interface IFuelTypeService
    {
        Task<FuelTypeResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<FuelTypeResponseDto>> GetAllAsync(bool onlyActive = false);
        Task<FuelTypeResponseDto> CreateAsync(FuelTypeCreateDto createDto);
        Task<FuelTypeResponseDto?> UpdateAsync(FuelTypeUpdateDto updateDto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ToggleActiveAsync(Guid id);
    }
}
