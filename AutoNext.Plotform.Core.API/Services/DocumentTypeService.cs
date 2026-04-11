using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AutoNext.Plotform.Core.API.Services
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DocumentTypeService> _logger;

        public DocumentTypeService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<DocumentTypeService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DocumentTypeResponseDto?> GetDocumentTypeByIdAsync(Guid documentTypeId)
        {
            try
            {
                var documentType = await _unitOfWork.DocumentTypes.GetByIdAsync(documentTypeId);
                if (documentType == null)
                    return null;

                return MapToResponseDto(documentType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting document type by ID: {Id}", documentTypeId);
                throw;
            }
        }

        public async Task<DocumentTypeResponseDto?> GetDocumentTypeByCodeAsync(string code)
        {
            try
            {
                var documentTypes = await _unitOfWork.DocumentTypes
                    .FindAsync(dt => dt.Code == code);
                var documentType = documentTypes.FirstOrDefault();

                if (documentType == null)
                    return null;

                return MapToResponseDto(documentType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting document type by code: {Code}", code);
                throw;
            }
        }

        public async Task<IEnumerable<DocumentTypeResponseDto>> GetAllDocumentTypesAsync()
        {
            var documentTypes = await _unitOfWork.DocumentTypes.GetAllAsync();
            return documentTypes.Select(dt => MapToResponseDto(dt))
                .OrderBy(dt => dt.DisplayOrder);
        }

        public async Task<IEnumerable<DocumentTypeResponseDto>> GetActiveDocumentTypesAsync()
        {
            var documentTypes = await _unitOfWork.DocumentTypes
                .FindAsync(dt => dt.IsActive == true);
            return documentTypes.Select(dt => MapToResponseDto(dt))
                .OrderBy(dt => dt.DisplayOrder);
        }

        public async Task<IEnumerable<DocumentTypeResponseDto>> GetDocumentTypesByCategoryAsync(string category)
        {
            var documentTypes = await _unitOfWork.DocumentTypes
                .FindAsync(dt => dt.Category == category);
            return documentTypes.Select(dt => MapToResponseDto(dt))
                .OrderBy(dt => dt.DisplayOrder);
        }

        public async Task<IEnumerable<DocumentTypeResponseDto>> GetRequiredDocumentTypesAsync()
        {
            var documentTypes = await _unitOfWork.DocumentTypes
                .FindAsync(dt => dt.IsRequired == true && dt.IsActive == true);
            return documentTypes.Select(dt => MapToResponseDto(dt))
                .OrderBy(dt => dt.DisplayOrder);
        }

        public async Task<IEnumerable<DocumentTypeResponseDto>> GetVerifiableDocumentTypesAsync()
        {
            var documentTypes = await _unitOfWork.DocumentTypes
                .FindAsync(dt => dt.IsVerifiable == true && dt.IsActive == true);
            return documentTypes.Select(dt => MapToResponseDto(dt))
                .OrderBy(dt => dt.DisplayOrder);
        }

        public async Task<IEnumerable<DocumentTypeResponseDto>> GetDocumentTypesByVehicleTypeAsync(string vehicleType)
        {
            var allDocumentTypes = await _unitOfWork.DocumentTypes.GetAllAsync();
            var filteredDocumentTypes = allDocumentTypes.Where(dt =>
            {
                if (string.IsNullOrEmpty(dt.ApplicableVehicleTypes))
                    return false;

                try
                {
                    var vehicleTypes = JsonSerializer.Deserialize<List<string>>(dt.ApplicableVehicleTypes);
                    return vehicleTypes != null && vehicleTypes.Contains(vehicleType);
                }
                catch
                {
                    return false;
                }
            });

            return filteredDocumentTypes.Select(dt => MapToResponseDto(dt))
                .OrderBy(dt => dt.DisplayOrder);
        }

        public async Task<IEnumerable<DocumentTypeCategoryDto>> GetDocumentTypesGroupedByCategoryAsync()
        {
            var allDocumentTypes = await _unitOfWork.DocumentTypes.GetAllAsync();
            var activeDocumentTypes = allDocumentTypes.Where(dt => dt.IsActive == true);

            var grouped = activeDocumentTypes
                .GroupBy(dt => dt.Category ?? "Uncategorized")
                .Select(g => new DocumentTypeCategoryDto
                {
                    Category = g.Key,
                    Count = g.Count(),
                    DocumentTypes = g.Select(dt => MapToResponseDto(dt))
                        .OrderBy(dt => dt.DisplayOrder)
                        .ToList()
                })
                .OrderBy(g => g.Category);

            return grouped;
        }

        public async Task<DocumentTypeResponseDto> CreateDocumentTypeAsync(DocumentTypeCreateDto createDto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Check if code already exists
                var existingTypes = await _unitOfWork.DocumentTypes
                    .FindAsync(dt => dt.Code == createDto.Code);
                if (existingTypes.Any())
                {
                    throw new InvalidOperationException($"Document type with code '{createDto.Code}' already exists");
                }

                var documentType = new DocumentType
                {
                    Id = Guid.NewGuid(),
                    Name = createDto.Name,
                    Code = createDto.Code,
                    Category = createDto.Category,
                    IsRequired = createDto.IsRequired,
                    IsVerifiable = createDto.IsVerifiable,
                    ExpiryMonths = createDto.ExpiryMonths,
                    ApplicableVehicleTypes = createDto.ApplicableVehicleTypes != null
                        ? JsonSerializer.Serialize(createDto.ApplicableVehicleTypes)
                        : null,
                    DisplayOrder = createDto.DisplayOrder,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.DocumentTypes.AddAsync(documentType);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Created new document type: {Name} with code: {Code}",
                    documentType.Name, documentType.Code);

                return MapToResponseDto(documentType);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error creating document type");
                throw;
            }
        }

        public async Task<DocumentTypeResponseDto?> UpdateDocumentTypeAsync(Guid documentTypeId, DocumentTypeUpdateDto updateDto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var documentType = await _unitOfWork.DocumentTypes.GetByIdAsync(documentTypeId);
                if (documentType == null)
                    return null;

                // Check if code already exists (if code is being changed)
                if (!string.IsNullOrEmpty(updateDto.Code) && updateDto.Code != documentType.Code)
                {
                    var existingTypes = await _unitOfWork.DocumentTypes
                        .FindAsync(dt => dt.Code == updateDto.Code);
                    if (existingTypes.Any())
                    {
                        throw new InvalidOperationException($"Document type with code '{updateDto.Code}' already exists");
                    }
                }

                if (!string.IsNullOrEmpty(updateDto.Name))
                    documentType.Name = updateDto.Name;

                if (!string.IsNullOrEmpty(updateDto.Code))
                    documentType.Code = updateDto.Code;

                if (updateDto.Category != null)
                    documentType.Category = updateDto.Category;

                if (updateDto.IsRequired.HasValue)
                    documentType.IsRequired = updateDto.IsRequired.Value;

                if (updateDto.IsVerifiable.HasValue)
                    documentType.IsVerifiable = updateDto.IsVerifiable.Value;

                if (updateDto.ExpiryMonths.HasValue)
                    documentType.ExpiryMonths = updateDto.ExpiryMonths.Value;

                if (updateDto.ApplicableVehicleTypes != null)
                    documentType.ApplicableVehicleTypes = JsonSerializer.Serialize(updateDto.ApplicableVehicleTypes);

                if (updateDto.DisplayOrder.HasValue)
                    documentType.DisplayOrder = updateDto.DisplayOrder.Value;

                if (updateDto.IsActive.HasValue)
                    documentType.IsActive = updateDto.IsActive.Value;

                documentType.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.DocumentTypes.Update(documentType);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Updated document type: {Name} with ID: {Id}", documentType.Name, documentType.Id);

                return MapToResponseDto(documentType);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error updating document type with ID: {Id}", documentTypeId);
                throw;
            }
        }

        public async Task<bool> DeleteDocumentTypeAsync(Guid documentTypeId)
        {
            var documentType = await _unitOfWork.DocumentTypes.GetByIdAsync(documentTypeId);
            if (documentType == null)
                return false;

            _unitOfWork.DocumentTypes.Remove(documentType);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Deleted document type with ID: {Id}", documentTypeId);
            return true;
        }

        public async Task<bool> ToggleDocumentTypeStatusAsync(Guid documentTypeId, bool isActive)
        {
            var documentType = await _unitOfWork.DocumentTypes.GetByIdAsync(documentTypeId);
            if (documentType == null)
                return false;

            documentType.IsActive = isActive;
            documentType.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.DocumentTypes.Update(documentType);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Document type {Id} status changed to: {Status}", documentTypeId, isActive);
            return true;
        }

        public async Task<bool> ReorderDocumentTypesAsync(Dictionary<Guid, int> orderMap)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                foreach (var item in orderMap)
                {
                    var documentType = await _unitOfWork.DocumentTypes.GetByIdAsync(item.Key);
                    if (documentType != null)
                    {
                        documentType.DisplayOrder = item.Value;
                        documentType.UpdatedAt = DateTime.UtcNow;
                        _unitOfWork.DocumentTypes.Update(documentType);
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Reordered {Count} document types", orderMap.Count);
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error reordering document types");
                throw;
            }
        }

        public async Task<IEnumerable<string>> GetDistinctCategoriesAsync()
        {
            var allDocumentTypes = await _unitOfWork.DocumentTypes.GetAllAsync();
            return allDocumentTypes
                .Where(dt => !string.IsNullOrEmpty(dt.Category))
                .Select(dt => dt.Category!)
                .Distinct()
                .OrderBy(c => c);
        }

        private DocumentTypeResponseDto MapToResponseDto(DocumentType documentType)
        {
            return new DocumentTypeResponseDto
            {
                Id = documentType.Id,
                Name = documentType.Name,
                Code = documentType.Code,
                Category = documentType.Category,
                IsRequired = documentType.IsRequired,
                IsVerifiable = documentType.IsVerifiable,
                ExpiryMonths = documentType.ExpiryMonths,
                ApplicableVehicleTypes = !string.IsNullOrEmpty(documentType.ApplicableVehicleTypes)
                    ? JsonSerializer.Deserialize<List<string>>(documentType.ApplicableVehicleTypes)
                    : null,
                DisplayOrder = documentType.DisplayOrder,
                IsActive = documentType.IsActive,
                CreatedAt = documentType.CreatedAt,
                UpdatedAt = documentType.UpdatedAt
            };
        }
    }
}