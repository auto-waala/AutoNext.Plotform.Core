using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;

namespace AutoNext.Plotform.Core.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CategoryResponseDto?> GetCategoryByIdAsync(Guid categoryId)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
                if (category == null)
                    return null;

                // Load subcategories if needed
                var subCategories = await _unitOfWork.Categories
                    .FindAsync(c => c.ParentCategoryId == categoryId);
                category.SubCategories = subCategories.ToList();

                return _mapper.Map<CategoryResponseDto>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting category by ID: {Id}", categoryId);
                throw;
            }
        }

        public async Task<CategoryResponseDto?> GetCategoryByCodeAsync(string code)
        {
            try
            {
                var categories = await _unitOfWork.Categories
                    .FindAsync(c => c.Code == code);
                var category = categories.FirstOrDefault();

                if (category == null)
                    return null;

                var subCategories = await _unitOfWork.Categories
                    .FindAsync(c => c.ParentCategoryId == category.Id);
                category.SubCategories = subCategories.ToList();

                return _mapper.Map<CategoryResponseDto>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting category by code: {Code}", code);
                throw;
            }
        }

        public async Task<CategoryResponseDto?> GetCategoryBySlugAsync(string slug)
        {
            try
            {
                var categories = await _unitOfWork.Categories
                    .FindAsync(c => c.Slug == slug);
                var category = categories.FirstOrDefault();

                if (category == null)
                    return null;

                var subCategories = await _unitOfWork.Categories
                    .FindAsync(c => c.ParentCategoryId == category.Id);
                category.SubCategories = subCategories.ToList();

                return _mapper.Map<CategoryResponseDto>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting category by slug: {Slug}", slug);
                throw;
            }
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();

            foreach (var category in categories)
            {
                var subCategories = await _unitOfWork.Categories
                    .FindAsync(c => c.ParentCategoryId == category.Id);
                category.SubCategories = subCategories.ToList();
            }

            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories)
                .OrderBy(c => c.DisplayOrder);
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetActiveCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories
                .FindAsync(c => c.IsActive == true);

            foreach (var category in categories)
            {
                var subCategories = await _unitOfWork.Categories
                    .FindAsync(c => c.ParentCategoryId == category.Id && c.IsActive == true);
                category.SubCategories = subCategories.ToList();
            }

            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories)
                .OrderBy(c => c.DisplayOrder);
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetMainCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories
                .FindAsync(c => c.ParentCategoryId == null && c.IsActive == true);

            foreach (var category in categories)
            {
                var subCategories = await _unitOfWork.Categories
                    .FindAsync(c => c.ParentCategoryId == category.Id && c.IsActive == true);
                category.SubCategories = subCategories.ToList();
            }

            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories)
                .OrderBy(c => c.DisplayOrder);
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetSubCategoriesAsync(Guid parentCategoryId)
        {
            var categories = await _unitOfWork.Categories
                .FindAsync(c => c.ParentCategoryId == parentCategoryId && c.IsActive == true);

            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories)
                .OrderBy(c => c.DisplayOrder);
        }

        public async Task<IEnumerable<CategoryTreeDto>> GetCategoryTreeAsync()
        {
            var allCategories = await _unitOfWork.Categories
                .FindAsync(c => c.IsActive == true);

            var mainCategories = allCategories
                .Where(c => c.ParentCategoryId == null)
                .OrderBy(c => c.DisplayOrder);

            foreach (var category in allCategories)
            {
                var children = allCategories
                    .Where(c => c.ParentCategoryId == category.Id)
                    .OrderBy(c => c.DisplayOrder)
                    .ToList();
                category.SubCategories = children;
            }

            return _mapper.Map<IEnumerable<CategoryTreeDto>>(mainCategories);
        }

        public async Task<CategoryResponseDto> CreateCategoryAsync(CategoryCreateDto createDto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                if (createDto.ParentCategoryId.HasValue)
                {
                    var parentExists = await _unitOfWork.Categories
                        .GetByIdAsync(createDto.ParentCategoryId.Value);
                    if (parentExists == null)
                    {
                        throw new ArgumentException($"Parent category with ID {createDto.ParentCategoryId.Value} does not exist");
                    }
                }

                var category = _mapper.Map<Category>(createDto);
                category.Id = Guid.NewGuid();
                category.CreatedAt = DateTime.UtcNow;

                await _unitOfWork.Categories.AddAsync(category);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Created new category: {Name} with code: {Code}",
                    category.Name, category.Code);

                return _mapper.Map<CategoryResponseDto>(category);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error creating category");
                throw;
            }
        }

        public async Task<CategoryResponseDto?> UpdateCategoryAsync(Guid categoryId, CategoryUpdateDto updateDto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
                if (category == null)
                    return null;

                if (updateDto.ParentCategoryId.HasValue)
                {
                    if (updateDto.ParentCategoryId.Value == categoryId)
                    {
                        throw new ArgumentException("Category cannot be its own parent");
                    }

                    var parentExists = await _unitOfWork.Categories
                        .GetByIdAsync(updateDto.ParentCategoryId.Value);
                    if (parentExists == null)
                    {
                        throw new ArgumentException($"Parent category with ID {updateDto.ParentCategoryId.Value} does not exist");
                    }
                }

                _mapper.Map(updateDto, category);
                category.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.Categories.Update(category);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Updated category: {Name} with ID: {Id}", category.Name, category.Id);

                return _mapper.Map<CategoryResponseDto>(category);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error updating category with ID: {Id}", categoryId);
                throw;
            }
        }

        public async Task<bool> DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category == null)
                return false;

            var hasSubCategories = await _unitOfWork.Categories
                .AnyAsync(c => c.ParentCategoryId == categoryId);

            if (hasSubCategories)
            {
                throw new InvalidOperationException("Cannot delete category that has sub-categories");
            }

            _unitOfWork.Categories.Remove(category);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Deleted category with ID: {Id}", categoryId);
            return true;
        }

        public async Task<bool> ToggleCategoryStatusAsync(Guid categoryId, bool isActive)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category == null)
                return false;

            category.IsActive = isActive;
            category.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Category {Id} status changed to: {Status}", categoryId, isActive);
            return true;
        }

        public async Task<bool> ReorderCategoriesAsync(Dictionary<Guid, int> orderMap)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                foreach (var item in orderMap)
                {
                    var category = await _unitOfWork.Categories.GetByIdAsync(item.Key);
                    if (category != null)
                    {
                        category.DisplayOrder = item.Value;
                        category.UpdatedAt = DateTime.UtcNow;
                        _unitOfWork.Categories.Update(category);
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Reordered {Count} categories", orderMap.Count);
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error reordering categories");
                throw;
            }
        }
    }
}