using AutoNext.Plotform.Core.API.Models.DTOs;

namespace AutoNext.Plotform.Core.API.Services
{
    public interface IWarrantyTypeService
    {
        Task<WarrantyTypeResponseDto?> GetWarrantyTypeByIdAsync(Guid warrantyTypeId);
        Task<WarrantyTypeResponseDto?> GetWarrantyTypeByCodeAsync(string code);
        Task<IEnumerable<WarrantyTypeResponseDto>> GetAllWarrantyTypesAsync();
        Task<IEnumerable<WarrantyTypeResponseDto>> GetActiveWarrantyTypesAsync();
        Task<IEnumerable<WarrantyTypeResponseDto>> GetTransferableWarrantyTypesAsync();
        Task<IEnumerable<WarrantyTypeResponseDto>> GetWarrantyTypesByCategoryAsync(string categoryCode);
        Task<IEnumerable<WarrantyTypeResponseDto>> FilterWarrantyTypesAsync(WarrantyTypeFilterDto filterDto);
        Task<WarrantyTypeResponseDto> CreateWarrantyTypeAsync(WarrantyTypeCreateDto createDto);
        Task<WarrantyTypeResponseDto?> UpdateWarrantyTypeAsync(Guid warrantyTypeId, WarrantyTypeUpdateDto updateDto);
        Task<bool> DeleteWarrantyTypeAsync(Guid warrantyTypeId);
        Task<bool> ToggleWarrantyTypeStatusAsync(Guid warrantyTypeId, bool isActive);
        Task<bool> ReorderWarrantyTypesAsync(Dictionary<Guid, int> orderMap);
        Task<WarrantyComparisonDto> CompareWarrantiesAsync(Guid warrantyIdA, Guid warrantyIdB);
        Task<WarrantyTypeResponseDto?> GetBestWarrantyByCategoryAsync(string categoryCode);
    }
}