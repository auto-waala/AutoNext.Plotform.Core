using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AutoNext.Plotform.Core.API.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BrandService> _logger;

        public BrandService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<BrandService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BrandResponseDto?> GetBrandByIdAsync(Guid brandId)
        {
            try
            {
                var brand = await _unitOfWork.Brands.GetByIdAsync(brandId);
                if (brand == null)
                    return null;

                return MapToResponseDto(brand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting brand by ID: {Id}", brandId);
                throw;
            }
        }

        public async Task<IEnumerable<BrandResponseDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Brands.GetAllAsync();
            return brands.Select(b => MapToResponseDto(b));
        }

        public async Task<IEnumerable<BrandResponseDto>> GetActiveBrandsAsync()
        {
            var brands = await _unitOfWork.Brands
                .FindAsync(b => b.IsActive == true);
            return brands.OrderBy(b => b.DisplayOrder).Select(b => MapToResponseDto(b));
        }

        public async Task<IEnumerable<BrandResponseDto>> GetBrandsByCategoryAsync(string categoryCode)
        {
            var allBrands = await _unitOfWork.Brands.GetAllAsync();
            var filteredBrands = allBrands.Where(b =>
            {
                if (string.IsNullOrEmpty(b.ApplicableCategories))
                    return false;

                try
                {
                    var categories = JsonSerializer.Deserialize<List<string>>(b.ApplicableCategories);
                    return categories != null && categories.Contains(categoryCode);
                }
                catch
                {
                    return false;
                }
            });

            return filteredBrands.Select(b => MapToResponseDto(b));
        }

        public async Task<IEnumerable<BrandResponseDto>> GetBrandsByCountryAsync(string countryCode)
        {
            var brands = await _unitOfWork.Brands
                .FindAsync(b => b.CountryOfOrigin == countryCode);
            return brands.Select(b => MapToResponseDto(b));
        }

        public async Task<BrandResponseDto> CreateBrandAsync(BrandCreateDto createDto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var brand = new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = createDto.Name,
                    Code = createDto.Code,
                    Slug = createDto.Slug,
                    Description = createDto.Description,
                    LogoUrl = createDto.LogoUrl,
                    WebsiteUrl = createDto.WebsiteUrl,
                    CountryOfOrigin = createDto.CountryOfOrigin,
                    FoundedYear = createDto.FoundedYear,
                    ApplicableCategories = createDto.ApplicableCategories != null
                        ? JsonSerializer.Serialize(createDto.ApplicableCategories)
                        : null,
                    DisplayOrder = createDto.DisplayOrder,
                    IsActive = true,
                    Metadata = createDto.Metadata != null
                        ? JsonSerializer.Serialize(createDto.Metadata)
                        : null,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.Brands.AddAsync(brand);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Created new brand: {Name} with code: {Code}",
                    brand.Name, brand.Code);

                return MapToResponseDto(brand);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error creating brand");
                throw;
            }
        }

        public async Task<BrandResponseDto?> UpdateBrandAsync(Guid brandId, BrandUpdateDto updateDto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var brand = await _unitOfWork.Brands.GetByIdAsync(brandId);
                if (brand == null)
                    return null;

                if (!string.IsNullOrEmpty(updateDto.Name))
                    brand.Name = updateDto.Name;

                if (!string.IsNullOrEmpty(updateDto.Code))
                    brand.Code = updateDto.Code;

                if (!string.IsNullOrEmpty(updateDto.Slug))
                    brand.Slug = updateDto.Slug;

                if (updateDto.Description != null)
                    brand.Description = updateDto.Description;

                if (updateDto.LogoUrl != null)
                    brand.LogoUrl = updateDto.LogoUrl;

                if (updateDto.WebsiteUrl != null)
                    brand.WebsiteUrl = updateDto.WebsiteUrl;

                if (updateDto.CountryOfOrigin != null)
                    brand.CountryOfOrigin = updateDto.CountryOfOrigin;

                if (updateDto.FoundedYear.HasValue)
                    brand.FoundedYear = updateDto.FoundedYear;

                if (updateDto.ApplicableCategories != null)
                    brand.ApplicableCategories = JsonSerializer.Serialize(updateDto.ApplicableCategories);

                if (updateDto.DisplayOrder.HasValue)
                    brand.DisplayOrder = updateDto.DisplayOrder.Value;

                if (updateDto.IsActive.HasValue)
                    brand.IsActive = updateDto.IsActive.Value;

                if (updateDto.Metadata != null)
                    brand.Metadata = JsonSerializer.Serialize(updateDto.Metadata);

                brand.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.Brands.Update(brand);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Updated brand: {Name} with ID: {Id}", brand.Name, brand.Id);

                return MapToResponseDto(brand);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error updating brand with ID: {Id}", brandId);
                throw;
            }
        }

        public async Task<bool> DeleteBrandAsync(Guid brandId)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(brandId);
            if (brand == null)
                return false;

            _unitOfWork.Brands.Remove(brand);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Deleted brand with ID: {Id}", brandId);
            return true;
        }

        public async Task<bool> ToggleBrandStatusAsync(Guid brandId, bool isActive)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(brandId);
            if (brand == null)
                return false;

            brand.IsActive = isActive;
            brand.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Brands.Update(brand);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Brand {Id} status changed to: {Status}", brandId, isActive);
            return true;
        }

        private BrandResponseDto MapToResponseDto(Brand brand)
        {
            return new BrandResponseDto
            {
                Id = brand.Id,
                Name = brand.Name,
                Code = brand.Code,
                Slug = brand.Slug,
                Description = brand.Description,
                LogoUrl = brand.LogoUrl,
                WebsiteUrl = brand.WebsiteUrl,
                CountryOfOrigin = brand.CountryOfOrigin,
                FoundedYear = brand.FoundedYear,
                ApplicableCategories = !string.IsNullOrEmpty(brand.ApplicableCategories)
                    ? JsonSerializer.Deserialize<List<string>>(brand.ApplicableCategories)
                    : null,
                DisplayOrder = brand.DisplayOrder,
                IsActive = brand.IsActive,
                Metadata = !string.IsNullOrEmpty(brand.Metadata)
                    ? JsonSerializer.Deserialize<Dictionary<string, object>>(brand.Metadata)
                    : null,
                CreatedAt = brand.CreatedAt,
                UpdatedAt = brand.UpdatedAt
            };
        }
    }
}