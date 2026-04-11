using AutoNext.Plotform.Core.API.Models.DTOs;

namespace AutoNext.Plotform.Core.API.Services
{
    public interface IBrandService
    {
        Task<BrandResponseDto?> GetBrandByIdAsync(Guid brandId);
        Task<IEnumerable<BrandResponseDto>> GetAllBrandsAsync();
        Task<IEnumerable<BrandResponseDto>> GetActiveBrandsAsync();
        Task<IEnumerable<BrandResponseDto>> GetBrandsByCategoryAsync(string categoryCode);
        Task<IEnumerable<BrandResponseDto>> GetBrandsByCountryAsync(string countryCode);
        Task<BrandResponseDto> CreateBrandAsync(BrandCreateDto createDto);
        Task<BrandResponseDto?> UpdateBrandAsync(Guid brandId, BrandUpdateDto updateDto);
        Task<bool> DeleteBrandAsync(Guid brandId);
        Task<bool> ToggleBrandStatusAsync(Guid brandId, bool isActive);
    }
}