using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AutoNext.Plotform.Core.API.Services
{
    public class InspectionChecklistService : IInspectionChecklistService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<InspectionChecklistService> _logger;

        public InspectionChecklistService(IUnitOfWork unitOfWork, ILogger<InspectionChecklistService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<InspectionChecklistResponseDto>> GetAllAsync()
        {
            var data = await _unitOfWork.InspectionChecklists.GetAllAsync();
            return data.Select(Map);
        }

        public async Task<IEnumerable<InspectionChecklistResponseDto>> GetActiveAsync()
        {
            var data = await _unitOfWork.InspectionChecklists.FindAsync(x => x.IsActive);
            return data.Select(Map);
        }

        public async Task<InspectionChecklistResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.InspectionChecklists.GetByIdAsync(id);
            return entity == null ? null : Map(entity);
        }

        public async Task<IEnumerable<InspectionChecklistResponseDto>> GetByCategoryAsync(string category)
        {
            var data = await _unitOfWork.InspectionChecklists
                .FindAsync(x => x.Category == category);

            return data.Select(Map);
        }

        public async Task<InspectionChecklistResponseDto> CreateAsync(InspectionChecklistCreateDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var entity = new InspectionChecklist
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    Code = dto.Code,
                    Category = dto.Category,
                    ApplicableVehicleTypes = dto.ApplicableVehicleTypes != null
                        ? JsonSerializer.Serialize(dto.ApplicableVehicleTypes)
                        : null,
                    IsCritical = dto.IsCritical,
                    Weightage = dto.Weightage,
                    DisplayOrder = dto.DisplayOrder,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.InspectionChecklists.AddAsync(entity);
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

        public async Task<InspectionChecklistResponseDto?> UpdateAsync(Guid id, InspectionChecklistUpdateDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var entity = await _unitOfWork.InspectionChecklists.GetByIdAsync(id);
                if (entity == null) return null;

                if (dto.Name != null) entity.Name = dto.Name;
                if (dto.Code != null) entity.Code = dto.Code;
                if (dto.Category != null) entity.Category = dto.Category;

                if (dto.ApplicableVehicleTypes != null)
                    entity.ApplicableVehicleTypes = JsonSerializer.Serialize(dto.ApplicableVehicleTypes);

                if (dto.IsCritical.HasValue) entity.IsCritical = dto.IsCritical.Value;
                if (dto.Weightage.HasValue) entity.Weightage = dto.Weightage.Value;
                if (dto.DisplayOrder.HasValue) entity.DisplayOrder = dto.DisplayOrder.Value;
                if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;

                entity.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.InspectionChecklists.Update(entity);
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
            var entity = await _unitOfWork.InspectionChecklists.GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.InspectionChecklists.Remove(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleStatusAsync(Guid id, bool isActive)
        {
            var entity = await _unitOfWork.InspectionChecklists.GetByIdAsync(id);
            if (entity == null) return false;

            entity.IsActive = isActive;
            entity.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.InspectionChecklists.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        private InspectionChecklistResponseDto Map(InspectionChecklist x)
        {
            return new InspectionChecklistResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Category = x.Category,
                ApplicableVehicleTypes = !string.IsNullOrEmpty(x.ApplicableVehicleTypes)
                    ? JsonSerializer.Deserialize<List<string>>(x.ApplicableVehicleTypes)
                    : null,
                IsCritical = x.IsCritical,
                Weightage = x.Weightage,
                DisplayOrder = x.DisplayOrder,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            };
        }
    }
}