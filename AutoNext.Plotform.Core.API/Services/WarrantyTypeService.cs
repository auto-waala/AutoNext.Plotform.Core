using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;
using System.Text.Json;

namespace AutoNext.Plotform.Core.API.Services
{
    public class WarrantyTypeService : IWarrantyTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<WarrantyTypeService> _logger;

        public WarrantyTypeService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<WarrantyTypeService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<WarrantyTypeResponseDto?> GetWarrantyTypeByIdAsync(Guid warrantyTypeId)
        {
            try
            {
                var warrantyType = await _unitOfWork.WarrantyTypes.GetByIdAsync(warrantyTypeId);
                if (warrantyType == null)
                    return null;

                return MapToResponseDto(warrantyType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting warranty type by ID: {Id}", warrantyTypeId);
                throw;
            }
        }

        public async Task<WarrantyTypeResponseDto?> GetWarrantyTypeByCodeAsync(string code)
        {
            try
            {
                var warrantyTypes = await _unitOfWork.WarrantyTypes
                    .FindAsync(wt => wt.Code == code);
                var warrantyType = warrantyTypes.FirstOrDefault();

                if (warrantyType == null)
                    return null;

                return MapToResponseDto(warrantyType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting warranty type by code: {Code}", code);
                throw;
            }
        }

        public async Task<IEnumerable<WarrantyTypeResponseDto>> GetAllWarrantyTypesAsync()
        {
            var warrantyTypes = await _unitOfWork.WarrantyTypes.GetAllAsync();
            return warrantyTypes.Select(wt => MapToResponseDto(wt))
                .OrderBy(wt => wt.DisplayOrder);
        }

        public async Task<IEnumerable<WarrantyTypeResponseDto>> GetActiveWarrantyTypesAsync()
        {
            var warrantyTypes = await _unitOfWork.WarrantyTypes
                .FindAsync(wt => wt.IsActive == true);
            return warrantyTypes.Select(wt => MapToResponseDto(wt))
                .OrderBy(wt => wt.DisplayOrder);
        }

        public async Task<IEnumerable<WarrantyTypeResponseDto>> GetTransferableWarrantyTypesAsync()
        {
            var warrantyTypes = await _unitOfWork.WarrantyTypes
                .FindAsync(wt => wt.IsTransferable == true && wt.IsActive == true);
            return warrantyTypes.Select(wt => MapToResponseDto(wt))
                .OrderBy(wt => wt.DisplayOrder);
        }

        public async Task<IEnumerable<WarrantyTypeResponseDto>> GetWarrantyTypesByCategoryAsync(string categoryCode)
        {
            var allWarrantyTypes = await _unitOfWork.WarrantyTypes.GetAllAsync();
            var filteredWarrantyTypes = allWarrantyTypes.Where(wt =>
            {
                if (string.IsNullOrEmpty(wt.ApplicableCategories))
                    return false;

                try
                {
                    var categories = JsonSerializer.Deserialize<List<string>>(wt.ApplicableCategories);
                    return categories != null && categories.Contains(categoryCode);
                }
                catch
                {
                    return false;
                }
            });

            return filteredWarrantyTypes.Select(wt => MapToResponseDto(wt))
                .OrderBy(wt => wt.DisplayOrder);
        }

        public async Task<IEnumerable<WarrantyTypeResponseDto>> FilterWarrantyTypesAsync(WarrantyTypeFilterDto filterDto)
        {
            var allWarrantyTypes = await _unitOfWork.WarrantyTypes.GetAllAsync();

            var filteredWarrantyTypes = allWarrantyTypes.AsQueryable();

            if (filterDto.IsActive.HasValue)
                filteredWarrantyTypes = filteredWarrantyTypes.Where(wt => wt.IsActive == filterDto.IsActive.Value);

            if (filterDto.IsTransferable.HasValue)
                filteredWarrantyTypes = filteredWarrantyTypes.Where(wt => wt.IsTransferable == filterDto.IsTransferable.Value);

            if (filterDto.MinDurationMonths.HasValue)
                filteredWarrantyTypes = filteredWarrantyTypes.Where(wt => wt.DurationMonths >= filterDto.MinDurationMonths.Value);

            if (filterDto.MaxDurationMonths.HasValue)
                filteredWarrantyTypes = filteredWarrantyTypes.Where(wt => wt.DurationMonths <= filterDto.MaxDurationMonths.Value);

            if (filterDto.MinDurationKm.HasValue)
                filteredWarrantyTypes = filteredWarrantyTypes.Where(wt => wt.DurationKm >= filterDto.MinDurationKm.Value);

            if (filterDto.MaxDurationKm.HasValue)
                filteredWarrantyTypes = filteredWarrantyTypes.Where(wt => wt.DurationKm <= filterDto.MaxDurationKm.Value);


            return filteredWarrantyTypes.Select(wt => MapToResponseDto(wt))
                .OrderBy(wt => wt.DisplayOrder);
        }

        public async Task<WarrantyTypeResponseDto> CreateWarrantyTypeAsync(WarrantyTypeCreateDto createDto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Check if code already exists
                var existingTypes = await _unitOfWork.WarrantyTypes
                    .FindAsync(wt => wt.Code == createDto.Code);
                if (existingTypes.Any())
                {
                    throw new InvalidOperationException($"Warranty type with code '{createDto.Code}' already exists");
                }

                // Validate duration values
                if (createDto.DurationMonths.HasValue && createDto.DurationMonths.Value <= 0)
                    throw new ArgumentException("Duration months must be greater than 0");

                if (createDto.DurationKm.HasValue && createDto.DurationKm.Value <= 0)
                    throw new ArgumentException("Duration kilometers must be greater than 0");

                var warrantyType = new WarrantyType
                {
                    Id = Guid.NewGuid(),
                    Name = createDto.Name,
                    Code = createDto.Code,
                    Description = createDto.Description,
                    DurationMonths = createDto.DurationMonths,
                    DurationKm = createDto.DurationKm,
                    IsTransferable = createDto.IsTransferable,
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

                await _unitOfWork.WarrantyTypes.AddAsync(warrantyType);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Created new warranty type: {Name} with code: {Code}",
                    warrantyType.Name, warrantyType.Code);

                return MapToResponseDto(warrantyType);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error creating warranty type");
                throw;
            }
        }

        public async Task<WarrantyTypeResponseDto?> UpdateWarrantyTypeAsync(Guid warrantyTypeId, WarrantyTypeUpdateDto updateDto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var warrantyType = await _unitOfWork.WarrantyTypes.GetByIdAsync(warrantyTypeId);
                if (warrantyType == null)
                    return null;

                // Check if code already exists (if code is being changed)
                if (!string.IsNullOrEmpty(updateDto.Code) && updateDto.Code != warrantyType.Code)
                {
                    var existingTypes = await _unitOfWork.WarrantyTypes
                        .FindAsync(wt => wt.Code == updateDto.Code);
                    if (existingTypes.Any())
                    {
                        throw new InvalidOperationException($"Warranty type with code '{updateDto.Code}' already exists");
                    }
                    warrantyType.Code = updateDto.Code;
                }

                // Validate duration values
                if (updateDto.DurationMonths.HasValue && updateDto.DurationMonths.Value <= 0)
                    throw new ArgumentException("Duration months must be greater than 0");

                if (updateDto.DurationKm.HasValue && updateDto.DurationKm.Value <= 0)
                    throw new ArgumentException("Duration kilometers must be greater than 0");

                if (!string.IsNullOrEmpty(updateDto.Name))
                    warrantyType.Name = updateDto.Name;

                if (updateDto.Description != null)
                    warrantyType.Description = updateDto.Description;

                if (updateDto.DurationMonths.HasValue)
                    warrantyType.DurationMonths = updateDto.DurationMonths.Value;

                if (updateDto.DurationKm.HasValue)
                    warrantyType.DurationKm = updateDto.DurationKm.Value;

                if (updateDto.IsTransferable.HasValue)
                    warrantyType.IsTransferable = updateDto.IsTransferable.Value;

                if (updateDto.ApplicableCategories != null)
                    warrantyType.ApplicableCategories = JsonSerializer.Serialize(updateDto.ApplicableCategories);

                if (updateDto.DisplayOrder.HasValue)
                    warrantyType.DisplayOrder = updateDto.DisplayOrder.Value;

                if (updateDto.IsActive.HasValue)
                    warrantyType.IsActive = updateDto.IsActive.Value;

                if (updateDto.Metadata != null)
                    warrantyType.Metadata = JsonSerializer.Serialize(updateDto.Metadata);

                warrantyType.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.WarrantyTypes.Update(warrantyType);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Updated warranty type: {Name} with ID: {Id}", warrantyType.Name, warrantyType.Id);

                return MapToResponseDto(warrantyType);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error updating warranty type with ID: {Id}", warrantyTypeId);
                throw;
            }
        }

        public async Task<bool> DeleteWarrantyTypeAsync(Guid warrantyTypeId)
        {
            var warrantyType = await _unitOfWork.WarrantyTypes.GetByIdAsync(warrantyTypeId);
            if (warrantyType == null)
                return false;

            _unitOfWork.WarrantyTypes.Remove(warrantyType);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Deleted warranty type with ID: {Id}", warrantyTypeId);
            return true;
        }

        public async Task<bool> ToggleWarrantyTypeStatusAsync(Guid warrantyTypeId, bool isActive)
        {
            var warrantyType = await _unitOfWork.WarrantyTypes.GetByIdAsync(warrantyTypeId);
            if (warrantyType == null)
                return false;

            warrantyType.IsActive = isActive;
            warrantyType.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.WarrantyTypes.Update(warrantyType);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Warranty type {Id} status changed to: {Status}", warrantyTypeId, isActive);
            return true;
        }

        public async Task<bool> ReorderWarrantyTypesAsync(Dictionary<Guid, int> orderMap)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                foreach (var item in orderMap)
                {
                    var warrantyType = await _unitOfWork.WarrantyTypes.GetByIdAsync(item.Key);
                    if (warrantyType != null)
                    {
                        warrantyType.DisplayOrder = item.Value;
                        warrantyType.UpdatedAt = DateTime.UtcNow;
                        _unitOfWork.WarrantyTypes.Update(warrantyType);
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Reordered {Count} warranty types", orderMap.Count);
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error reordering warranty types");
                throw;
            }
        }

        public async Task<WarrantyComparisonDto> CompareWarrantiesAsync(Guid warrantyIdA, Guid warrantyIdB)
        {
            var warrantyA = await _unitOfWork.WarrantyTypes.GetByIdAsync(warrantyIdA);
            var warrantyB = await _unitOfWork.WarrantyTypes.GetByIdAsync(warrantyIdB);

            if (warrantyA == null || warrantyB == null)
                throw new ArgumentException("One or both warranty types not found");

            var comparison = new WarrantyComparisonResultDto
            {
                SameDuration = warrantyA.DurationMonths == warrantyB.DurationMonths &&
                               warrantyA.DurationKm == warrantyB.DurationKm,
                SameTransferability = warrantyA.IsTransferable == warrantyB.IsTransferable,
                DurationDifferenceMonths = (warrantyA.DurationMonths ?? 0) - (warrantyB.DurationMonths ?? 0),
                DurationDifferenceKm = (warrantyA.DurationKm ?? 0) - (warrantyB.DurationKm ?? 0)
            };

            // Determine better coverage
            var aScore = (warrantyA.DurationMonths ?? 0) + ((warrantyA.DurationKm ?? 0) / 1000);
            var bScore = (warrantyB.DurationMonths ?? 0) + ((warrantyB.DurationKm ?? 0) / 1000);

            comparison.BetterCoverage = aScore > bScore ? warrantyA.Name :
                                        bScore > aScore ? warrantyB.Name :
                                        "Equal";

            return new WarrantyComparisonDto
            {
                WarrantyA = MapToResponseDto(warrantyA),
                WarrantyB = MapToResponseDto(warrantyB),
                Comparison = comparison
            };
        }

        public async Task<WarrantyTypeResponseDto?> GetBestWarrantyByCategoryAsync(string categoryCode)
        {
            var warranties = await GetWarrantyTypesByCategoryAsync(categoryCode);

            if (!warranties.Any())
                return null;

            // Calculate a score based on duration and transferability
            return warranties.OrderByDescending(w =>
                (w.DurationMonths ?? 0) +
                ((w.DurationKm ?? 0) / 1000) +
                (w.IsTransferable ? 100 : 0)
            ).FirstOrDefault();
        }

        private WarrantyTypeResponseDto MapToResponseDto(WarrantyType warrantyType)
        {
            var dto = new WarrantyTypeResponseDto
            {
                Id = warrantyType.Id,
                Name = warrantyType.Name,
                Code = warrantyType.Code,
                Description = warrantyType.Description,
                DurationMonths = warrantyType.DurationMonths,
                DurationKm = warrantyType.DurationKm,
                IsTransferable = warrantyType.IsTransferable,
                ApplicableCategories = !string.IsNullOrEmpty(warrantyType.ApplicableCategories)
                    ? JsonSerializer.Deserialize<List<string>>(warrantyType.ApplicableCategories)
                    : null,
                DisplayOrder = warrantyType.DisplayOrder,
                IsActive = warrantyType.IsActive,
                Metadata = !string.IsNullOrEmpty(warrantyType.Metadata)
                    ? JsonSerializer.Deserialize<Dictionary<string, object>>(warrantyType.Metadata)
                    : null,
                CreatedAt = warrantyType.CreatedAt,
                UpdatedAt = warrantyType.UpdatedAt
            };

            // Format duration string
            var durationParts = new List<string>();
            if (dto.DurationMonths.HasValue)
                durationParts.Add($"{dto.DurationMonths.Value} months");
            if (dto.DurationKm.HasValue)
                durationParts.Add($"{dto.DurationKm.Value:N0} km");

            dto.FormattedDuration = durationParts.Any()
                ? string.Join(" or ", durationParts)
                : "Not specified";

            return dto;
        }
    }
}