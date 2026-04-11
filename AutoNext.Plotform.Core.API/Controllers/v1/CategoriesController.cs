using Asp.Versioning;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoNext.Plotform.Core.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(
            ICategoryService categoryService,
            ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all categories")]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("active")]
        [SwaggerOperation(Summary = "Get all active categories")]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActive()
        {
            var categories = await _categoryService.GetActiveCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("main")]
        [SwaggerOperation(Summary = "Get main categories (no parent)")]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMainCategories()
        {
            var categories = await _categoryService.GetMainCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("tree")]
        [SwaggerOperation(Summary = "Get category hierarchy tree")]
        [ProducesResponseType(typeof(IEnumerable<CategoryTreeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryTree()
        {
            var tree = await _categoryService.GetCategoryTreeAsync();
            return Ok(tree);
        }

        [HttpGet("{categoryId:Guid}")]
        [SwaggerOperation(Summary = "Get category by ID")]
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid categoryId)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            if (category == null)
                return NotFound($"Category with ID {categoryId} not found");
            return Ok(category);
        }

        [HttpGet("code/{code}")]
        [SwaggerOperation(Summary = "Get category by code")]
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCode(string code)
        {
            var category = await _categoryService.GetCategoryByCodeAsync(code);
            if (category == null)
                return NotFound($"Category with code {code} not found");
            return Ok(category);
        }

        [HttpGet("slug/{slug}")]
        [SwaggerOperation(Summary = "Get category by slug")]
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var category = await _categoryService.GetCategoryBySlugAsync(slug);
            if (category == null)
                return NotFound($"Category with slug {slug} not found");
            return Ok(category);
        }

        [HttpGet("{parentCategoryId:Guid}/subcategories")]
        [SwaggerOperation(Summary = "Get subcategories by parent ID")]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSubCategories(Guid parentCategoryId)
        {
            var categories = await _categoryService.GetSubCategoriesAsync(parentCategoryId);
            return Ok(categories);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new category")]
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryService.CreateCategoryAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { version = "1.0", categoryId = category.Id }, category);
        }

        [HttpPut("{categoryId:Guid}")]
        [SwaggerOperation(Summary = "Update category")]
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid categoryId, [FromBody] CategoryUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryService.UpdateCategoryAsync(categoryId, updateDto);
            if (category == null)
                return NotFound($"Category with ID {categoryId} not found");

            return Ok(category);
        }

        [HttpPatch("{categoryId:Guid}/toggle/{isActive}")]
        [SwaggerOperation(Summary = "Toggle category active status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ToggleStatus(Guid categoryId, bool isActive)
        {
            var result = await _categoryService.ToggleCategoryStatusAsync(categoryId, isActive);
            if (!result)
                return NotFound($"Category with ID {categoryId} not found");

            return NoContent();
        }

        [HttpPost("reorder")]
        [SwaggerOperation(Summary = "Reorder categories")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Reorder([FromBody] Dictionary<Guid, int> orderMap)
        {
            if (orderMap == null || !orderMap.Any())
                return BadRequest("Order map cannot be empty");

            await _categoryService.ReorderCategoriesAsync(orderMap);
            return NoContent();
        }

        [HttpDelete("{categoryId:Guid}")]
        [SwaggerOperation(Summary = "Delete category")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid categoryId)
        {
            try
            {
                var deleted = await _categoryService.DeleteCategoryAsync(categoryId);
                if (!deleted)
                    return NotFound($"Category with ID {categoryId} not found");

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}