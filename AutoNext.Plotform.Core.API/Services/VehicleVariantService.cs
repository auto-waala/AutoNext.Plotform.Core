using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AutoNext.Plotform.Core.API.Services
{
    public class VehicleVariantService : IVehicleVariantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<VehicleVariantService> _logger;

        public VehicleVariantService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<VehicleVariantService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<VehicleVariantResponseDto?> GetVariantByIdAsync(Guid variantId)
        {
            try
            {
                var variant = await _unitOfWork.VehicleVariants.GetByIdAsync(variantId);
                if (variant == null)
                    return null;

                return await MapToResponseDto(variant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting variant by ID: {Id}", variantId);
                throw;
            }
        }

        public async Task<VehicleVariantResponseDto?> GetVariantByCodeAsync(string code)
        {
            try
            {
                var variants = await _unitOfWork.VehicleVariants
                    .FindAsync(v => v.Code == code);
                var variant = variants.FirstOrDefault();

                if (variant == null)
                    return null;

                return await MapToResponseDto(variant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting variant by code: {Code}", code);
                throw;
            }
        }

        public async Task<IEnumerable<VehicleVariantResponseDto>> GetAllVariantsAsync()
        {
            var variants = await _unitOfWork.VehicleVariants.GetAllAsync();
            var result = new List<VehicleVariantResponseDto>();

            foreach (var variant in variants)
            {
                result.Add(await MapToResponseDto(variant));
            }

            return result.OrderBy(v => v.DisplayOrder);
        }

        public async Task<IEnumerable<VehicleVariantResponseDto>> GetActiveVariantsAsync()
        {
            var variants = await _unitOfWork.VehicleVariants
                .FindAsync(v => v.IsActive == true);

            var result = new List<VehicleVariantResponseDto>();
            foreach (var variant in variants)
            {
                result.Add(await MapToResponseDto(variant));
            }

            return result.OrderBy(v => v.DisplayOrder);
        }

        public async Task<IEnumerable<VehicleVariantResponseDto>> GetVariantsByModelAsync(Guid modelId)
        {
            var variants = await _unitOfWork.VehicleVariants
                .FindAsync(v => v.ModelId == modelId);

            var result = new List<VehicleVariantResponseDto>();
            foreach (var variant in variants)
            {
                result.Add(await MapToResponseDto(variant));
            }

            return result.OrderBy(v => v.DisplayOrder);
        }

        public async Task<IEnumerable<VehicleVariantResponseDto>> GetAvailableVariantsAsync()
        {
            var variants = await _unitOfWork.VehicleVariants
                .FindAsync(v => v.IsAvailable == true && v.IsActive == true);

            var result = new List<VehicleVariantResponseDto>();
            foreach (var variant in variants)
            {
                result.Add(await MapToResponseDto(variant));
            }

            return result.OrderBy(v => v.DisplayOrder);
        }

        public async Task<IEnumerable<VehicleVariantResponseDto>> FilterVariantsAsync(VehicleVariantFilterDto filterDto)
        {
            var allVariants = await _unitOfWork.VehicleVariants.GetAllAsync();

            var filteredVariants = allVariants.AsQueryable();

            if (filterDto.ModelId.HasValue)
                filteredVariants = filteredVariants.Where(v => v.ModelId == filterDto.ModelId.Value);

            if (filterDto.FuelTypeId.HasValue)
                filteredVariants = filteredVariants.Where(v => v.FuelTypeId == filterDto.FuelTypeId.Value);

            if (filterDto.TransmissionId.HasValue)
                filteredVariants = filteredVariants.Where(v => v.TransmissionId == filterDto.TransmissionId.Value);

            if (!string.IsNullOrEmpty(filterDto.DriveType))
                filteredVariants = filteredVariants.Where(v => v.DriveType == filterDto.DriveType);

            if (filterDto.MinPrice.HasValue)
                filteredVariants = filteredVariants.Where(v => v.BasePrice >= filterDto.MinPrice.Value);

            if (filterDto.MaxPrice.HasValue)
                filteredVariants = filteredVariants.Where(v => v.BasePrice <= filterDto.MaxPrice.Value);

            if (filterDto.MinHorsepower.HasValue)
                filteredVariants = filteredVariants.Where(v => v.Horsepower >= filterDto.MinHorsepower.Value);

            if (filterDto.MaxHorsepower.HasValue)
                filteredVariants = filteredVariants.Where(v => v.Horsepower <= filterDto.MaxHorsepower.Value);

            if (filterDto.IsAvailable.HasValue)
                filteredVariants = filteredVariants.Where(v => v.IsAvailable == filterDto.IsAvailable.Value);

            if (filterDto.IsActive.HasValue)
                filteredVariants = filteredVariants.Where(v => v.IsActive == filterDto.IsActive.Value);

            if (filterDto.MinSeatingCapacity.HasValue)
                filteredVariants = filteredVariants.Where(v => v.SeatingCapacity >= filterDto.MinSeatingCapacity.Value);

            if (filterDto.MaxSeatingCapacity.HasValue)
                filteredVariants = filteredVariants.Where(v => v.SeatingCapacity <= filterDto.MaxSeatingCapacity.Value);

            var result = new List<VehicleVariantResponseDto>();
            foreach (var variant in filteredVariants)
            {
                result.Add(await MapToResponseDto(variant));
            }

            return result.OrderBy(v => v.DisplayOrder);
        }

        public async Task<VehicleVariantResponseDto> CreateVariantAsync(VehicleVariantCreateDto createDto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Validate model exists
                var model = await _unitOfWork.VehicleModels.GetByIdAsync(createDto.ModelId);
                if (model == null)
                {
                    throw new ArgumentException($"Model with ID {createDto.ModelId} does not exist");
                }

                // Check if variant code already exists for this model
                var existingVariants = await _unitOfWork.VehicleVariants
                    .FindAsync(v => v.ModelId == createDto.ModelId && v.Code == createDto.Code);
                if (existingVariants.Any())
                {
                    throw new InvalidOperationException($"Variant with code '{createDto.Code}' already exists for this model");
                }

                // Validate fuel type if provided
                if (createDto.FuelTypeId.HasValue)
                {
                    var fuelType = await _unitOfWork.FuelTypes.GetByIdAsync(createDto.FuelTypeId.Value);
                    if (fuelType == null)
                    {
                        throw new ArgumentException($"Fuel type with ID {createDto.FuelTypeId.Value} does not exist");
                    }
                }

                // Validate transmission if provided
                if (createDto.TransmissionId.HasValue)
                {
                    var transmission = await _unitOfWork.Transmissions.GetByIdAsync(createDto.TransmissionId.Value);
                    if (transmission == null)
                    {
                        throw new ArgumentException($"Transmission with ID {createDto.TransmissionId.Value} does not exist");
                    }
                }

                var variant = new VehicleVariant
                {
                    Id = Guid.NewGuid(),
                    ModelId = createDto.ModelId,
                    Name = createDto.Name,
                    Code = createDto.Code,
                    Description = createDto.Description,
                    FuelTypeId = createDto.FuelTypeId,
                    TransmissionId = createDto.TransmissionId,
                    DriveType = createDto.DriveType,
                    EngineSize = createDto.EngineSize,
                    Horsepower = createDto.Horsepower,
                    Torque = createDto.Torque,
                    SeatingCapacity = createDto.SeatingCapacity,
                    DoorsCount = createDto.DoorsCount,
                    BasePrice = createDto.BasePrice,
                    IsAvailable = createDto.IsAvailable,
                    DisplayOrder = createDto.DisplayOrder,
                    IsActive = true,
                    Metadata = createDto.Metadata != null
                        ? JsonSerializer.Serialize(createDto.Metadata)
                        : null,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.VehicleVariants.AddAsync(variant);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Created new variant: {Name} for model: {ModelId}",
                    variant.Name, variant.ModelId);

                return await MapToResponseDto(variant);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error creating variant");
                throw;
            }
        }

        public async Task<VehicleVariantResponseDto?> UpdateVariantAsync(Guid variantId, VehicleVariantUpdateDto updateDto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var variant = await _unitOfWork.VehicleVariants.GetByIdAsync(variantId);
                if (variant == null)
                    return null;

                // Validate model if being updated
                if (updateDto.ModelId.HasValue && updateDto.ModelId.Value != variant.ModelId)
                {
                    var model = await _unitOfWork.VehicleModels.GetByIdAsync(updateDto.ModelId.Value);
                    if (model == null)
                    {
                        throw new ArgumentException($"Model with ID {updateDto.ModelId.Value} does not exist");
                    }
                    variant.ModelId = updateDto.ModelId.Value;
                }

                // Check code uniqueness if being updated
                if (!string.IsNullOrEmpty(updateDto.Code) && updateDto.Code != variant.Code)
                {
                    var existingVariants = await _unitOfWork.VehicleVariants
                        .FindAsync(v => v.ModelId == variant.ModelId && v.Code == updateDto.Code);
                    if (existingVariants.Any())
                    {
                        throw new InvalidOperationException($"Variant with code '{updateDto.Code}' already exists for this model");
                    }
                    variant.Code = updateDto.Code;
                }

                // Validate fuel type if being updated
                if (updateDto.FuelTypeId.HasValue && updateDto.FuelTypeId != variant.FuelTypeId)
                {
                    var fuelType = await _unitOfWork.FuelTypes.GetByIdAsync(updateDto.FuelTypeId.Value);
                    if (fuelType == null)
                    {
                        throw new ArgumentException($"Fuel type with ID {updateDto.FuelTypeId.Value} does not exist");
                    }
                    variant.FuelTypeId = updateDto.FuelTypeId;
                }

                // Validate transmission if being updated
                if (updateDto.TransmissionId.HasValue && updateDto.TransmissionId != variant.TransmissionId)
                {
                    var transmission = await _unitOfWork.Transmissions.GetByIdAsync(updateDto.TransmissionId.Value);
                    if (transmission == null)
                    {
                        throw new ArgumentException($"Transmission with ID {updateDto.TransmissionId.Value} does not exist");
                    }
                    variant.TransmissionId = updateDto.TransmissionId;
                }

                if (!string.IsNullOrEmpty(updateDto.Name))
                    variant.Name = updateDto.Name;

                if (updateDto.Description != null)
                    variant.Description = updateDto.Description;

                if (updateDto.DriveType != null)
                    variant.DriveType = updateDto.DriveType;

                if (updateDto.EngineSize.HasValue)
                    variant.EngineSize = updateDto.EngineSize.Value;

                if (updateDto.Horsepower.HasValue)
                    variant.Horsepower = updateDto.Horsepower.Value;

                if (updateDto.Torque.HasValue)
                    variant.Torque = updateDto.Torque.Value;

                if (updateDto.SeatingCapacity.HasValue)
                    variant.SeatingCapacity = updateDto.SeatingCapacity.Value;

                if (updateDto.DoorsCount.HasValue)
                    variant.DoorsCount = updateDto.DoorsCount.Value;

                if (updateDto.BasePrice.HasValue)
                    variant.BasePrice = updateDto.BasePrice.Value;

                if (updateDto.IsAvailable.HasValue)
                    variant.IsAvailable = updateDto.IsAvailable.Value;

                if (updateDto.DisplayOrder.HasValue)
                    variant.DisplayOrder = updateDto.DisplayOrder.Value;

                if (updateDto.IsActive.HasValue)
                    variant.IsActive = updateDto.IsActive.Value;

                if (updateDto.Metadata != null)
                    variant.Metadata = JsonSerializer.Serialize(updateDto.Metadata);

                variant.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.VehicleVariants.Update(variant);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Updated variant: {Name} with ID: {Id}", variant.Name, variant.Id);

                return await MapToResponseDto(variant);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error updating variant with ID: {Id}", variantId);
                throw;
            }
        }

        public async Task<bool> DeleteVariantAsync(Guid variantId)
        {
            var variant = await _unitOfWork.VehicleVariants.GetByIdAsync(variantId);
            if (variant == null)
                return false;

            _unitOfWork.VehicleVariants.Remove(variant);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Deleted variant with ID: {Id}", variantId);
            return true;
        }

        public async Task<bool> ToggleVariantStatusAsync(Guid variantId, bool isActive)
        {
            var variant = await _unitOfWork.VehicleVariants.GetByIdAsync(variantId);
            if (variant == null)
                return false;

            variant.IsActive = isActive;
            variant.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.VehicleVariants.Update(variant);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Variant {Id} status changed to: {Status}", variantId, isActive);
            return true;
        }

        public async Task<bool> ToggleAvailabilityAsync(Guid variantId, bool isAvailable)
        {
            var variant = await _unitOfWork.VehicleVariants.GetByIdAsync(variantId);
            if (variant == null)
                return false;

            variant.IsAvailable = isAvailable;
            variant.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.VehicleVariants.Update(variant);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Variant {Id} availability changed to: {Available}", variantId, isAvailable);
            return true;
        }

        public async Task<bool> ReorderVariantsAsync(Dictionary<Guid, int> orderMap)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                foreach (var item in orderMap)
                {
                    var variant = await _unitOfWork.VehicleVariants.GetByIdAsync(item.Key);
                    if (variant != null)
                    {
                        variant.DisplayOrder = item.Value;
                        variant.UpdatedAt = DateTime.UtcNow;
                        _unitOfWork.VehicleVariants.Update(variant);
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Reordered {Count} variants", orderMap.Count);
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error reordering variants");
                throw;
            }
        }

        public async Task<VehicleVariantSpecsDto> GetVariantSpecsAsync(Guid variantId)
        {
            var variant = await _unitOfWork.VehicleVariants.GetByIdAsync(variantId);
            if (variant == null)
                return new VehicleVariantSpecsDto();

            var specs = new VehicleVariantSpecsDto
            {
                EngineSize = variant.EngineSize,
                Horsepower = variant.Horsepower,
                Torque = variant.Torque,
                SeatingCapacity = variant.SeatingCapacity,
                DoorsCount = variant.DoorsCount,
                DriveType = variant.DriveType,
                BasePrice = variant.BasePrice
            };

            if (variant.FuelTypeId.HasValue)
            {
                var fuelType = await _unitOfWork.FuelTypes.GetByIdAsync(variant.FuelTypeId.Value);
                specs.FuelType = fuelType?.Name;
            }

            if (variant.TransmissionId.HasValue)
            {
                var transmission = await _unitOfWork.Transmissions.GetByIdAsync(variant.TransmissionId.Value);
                specs.Transmission = transmission?.Name;
            }

            return specs;
        }

        public async Task<decimal> GetVariantPriceRangeAsync(Guid modelId)
        {
            var variants = await _unitOfWork.VehicleVariants
                .FindAsync(v => v.ModelId == modelId && v.BasePrice.HasValue);

            if (!variants.Any())
                return 0;

            var minPrice = variants.Min(v => v.BasePrice) ?? 0;
            var maxPrice = variants.Max(v => v.BasePrice) ?? 0;

            return maxPrice - minPrice;
        }

        private async Task<VehicleVariantResponseDto> MapToResponseDto(VehicleVariant variant)
        {
            var dto = new VehicleVariantResponseDto
            {
                Id = variant.Id,
                ModelId = variant.ModelId,
                Name = variant.Name,
                Code = variant.Code,
                Description = variant.Description,
                FuelTypeId = variant.FuelTypeId,
                TransmissionId = variant.TransmissionId,
                DriveType = variant.DriveType,
                EngineSize = variant.EngineSize,
                Horsepower = variant.Horsepower,
                Torque = variant.Torque,
                SeatingCapacity = variant.SeatingCapacity,
                DoorsCount = variant.DoorsCount,
                BasePrice = variant.BasePrice,
                IsAvailable = variant.IsAvailable,
                DisplayOrder = variant.DisplayOrder,
                IsActive = variant.IsActive,
                Metadata = !string.IsNullOrEmpty(variant.Metadata)
                    ? JsonSerializer.Deserialize<Dictionary<string, object>>(variant.Metadata)
                    : null,
                CreatedAt = variant.CreatedAt,
                UpdatedAt = variant.UpdatedAt
            };

            // Load model details
            var model = await _unitOfWork.VehicleModels.GetByIdAsync(variant.ModelId);
            if (model != null)
            {
                dto.ModelName = model.Name;
                dto.ModelCode = model.Code;
            }

            // Load fuel type details
            if (variant.FuelTypeId.HasValue)
            {
                var fuelType = await _unitOfWork.FuelTypes.GetByIdAsync(variant.FuelTypeId.Value);
                dto.FuelTypeName = fuelType?.Name;
            }

            // Load transmission details
            if (variant.TransmissionId.HasValue)
            {
                var transmission = await _unitOfWork.Transmissions.GetByIdAsync(variant.TransmissionId.Value);
                dto.TransmissionName = transmission?.Name;
            }

            return dto;
        }
    }
}