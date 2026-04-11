using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AutoNext.Plotform.Core.API.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FeatureService> _logger;

        public FeatureService(IUnitOfWork unitOfWork, ILogger<FeatureService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<FeatureResponseDto>> GetAllAsync()
        {
            var data = await _unitOfWork.Features.GetAllAsync();
            return data.Select(Map);
        }

        public async Task<IEnumerable<FeatureResponseDto>> GetActiveAsync()
        {
            var data = await _unitOfWork.Features.FindAsync(x => x.IsActive);
            return data.Select(Map);
        }

        public async Task<FeatureResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.Features.GetByIdAsync(id);
            return entity == null ? null : Map(entity);
        }

        public async Task<IEnumerable<FeatureResponseDto>> GetByCategoryAsync(string category)
        {
            var data = await _unitOfWork.Features
                .FindAsync(x => x.Category == category);

            return data.Select(Map);
        }

        public async Task<FeatureResponseDto> CreateAsync(FeatureCreateDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var entity = new Feature
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    Code = dto.Code,
                    Category = dto.Category,
                    SubCategory = dto.SubCategory,
                    IconUrl = dto.IconUrl,
                    ApplicableCategories = dto.ApplicableCategories != null
                        ? JsonSerializer.Serialize(dto.ApplicableCategories)
                        : null,
                    IsStandard = dto.IsStandard,
                    DisplayOrder = dto.DisplayOrder,
                    IsActive = true,
                    Metadata = dto.Metadata != null
                        ? JsonSerializer.Serialize(dto.Metadata)
                        : null,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.Features.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return Map(entity);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<FeatureResponseDto?> UpdateAsync(Guid id, FeatureUpdateDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var entity = await _unitOfWork.Features.GetByIdAsync(id);
                if (entity == null) return null;

                if (dto.Name != null) entity.Name = dto.Name;
                if (dto.Code != null) entity.Code = dto.Code;
                if (dto.Category != null) entity.Category = dto.Category;
                if (dto.SubCategory != null) entity.SubCategory = dto.SubCategory;
                if (dto.IconUrl != null) entity.IconUrl = dto.IconUrl;

                if (dto.ApplicableCategories != null)
                    entity.ApplicableCategories = JsonSerializer.Serialize(dto.ApplicableCategories);

                if (dto.IsStandard.HasValue) entity.IsStandard = dto.IsStandard.Value;
                if (dto.DisplayOrder.HasValue) entity.DisplayOrder = dto.DisplayOrder.Value;
                if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;

                if (dto.Metadata != null)
                    entity.Metadata = JsonSerializer.Serialize(dto.Metadata);

                entity.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.Features.Update(entity);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return Map(entity);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _unitOfWork.Features.GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.Features.Remove(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleStatusAsync(Guid id, bool isActive)
        {
            var entity = await _unitOfWork.Features.GetByIdAsync(id);
            if (entity == null) return false;

            entity.IsActive = isActive;
            entity.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Features.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        private FeatureResponseDto Map(Feature f)
        {
            return new FeatureResponseDto
            {
                Id = f.Id,
                Name = f.Name,
                Code = f.Code,
                Category = f.Category,
                SubCategory = f.SubCategory,
                IconUrl = f.IconUrl,
                ApplicableCategories = !string.IsNullOrEmpty(f.ApplicableCategories)
                    ? JsonSerializer.Deserialize<List<string>>(f.ApplicableCategories)
                    : null,
                IsStandard = f.IsStandard,
                DisplayOrder = f.DisplayOrder,
                IsActive = f.IsActive,
                Metadata = !string.IsNullOrEmpty(f.Metadata)
                    ? JsonSerializer.Deserialize<Dictionary<string, object>>(f.Metadata)
                    : null,
                CreatedAt = f.CreatedAt,
                UpdatedAt = f.UpdatedAt
            };
        }
    }
}