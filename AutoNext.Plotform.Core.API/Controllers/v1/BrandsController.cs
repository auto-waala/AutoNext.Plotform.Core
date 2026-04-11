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
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly ILogger<BrandsController> _logger;

        public BrandsController(
            IBrandService brandService,
            ILogger<BrandsController> logger)
        {
            _brandService = brandService;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all brands")]
        [ProducesResponseType(typeof(IEnumerable<BrandResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _brandService.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("active")]
        [SwaggerOperation(Summary = "Get all active brands")]
        [ProducesResponseType(typeof(IEnumerable<BrandResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActive()
        {
            var brands = await _brandService.GetActiveBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("{brandId:Guid}")]
        [SwaggerOperation(Summary = "Get brand by ID")]
        [ProducesResponseType(typeof(BrandResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid brandId)
        {
            var brand = await _brandService.GetBrandByIdAsync(brandId);
            if (brand == null)
                return NotFound($"Brand with ID {brandId} not found");
            return Ok(brand);
        }

        [HttpGet("category/{categoryCode}")]
        [SwaggerOperation(Summary = "Get brands by category")]
        [ProducesResponseType(typeof(IEnumerable<BrandResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCategory(string categoryCode)
        {
            var brands = await _brandService.GetBrandsByCategoryAsync(categoryCode);
            return Ok(brands);
        }

        [HttpGet("country/{countryCode}")]
        [SwaggerOperation(Summary = "Get brands by country of origin")]
        [ProducesResponseType(typeof(IEnumerable<BrandResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCountry(string countryCode)
        {
            var brands = await _brandService.GetBrandsByCountryAsync(countryCode);
            return Ok(brands);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new brand")]
        [ProducesResponseType(typeof(BrandResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] BrandCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var brand = await _brandService.CreateBrandAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { version = "1.0", brandId = brand.Id }, brand);
        }

        [HttpPut("{brandId:Guid}")]
        [SwaggerOperation(Summary = "Update brand")]
        [ProducesResponseType(typeof(BrandResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid brandId, [FromBody] BrandUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var brand = await _brandService.UpdateBrandAsync(brandId, updateDto);
            if (brand == null)
                return NotFound($"Brand with ID {brandId} not found");

            return Ok(brand);
        }

        [HttpPatch("{brandId:Guid}/toggle/{isActive}")]
        [SwaggerOperation(Summary = "Toggle brand active status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ToggleStatus(Guid brandId, bool isActive)
        {
            var result = await _brandService.ToggleBrandStatusAsync(brandId, isActive);
            if (!result)
                return NotFound($"Brand with ID {brandId} not found");

            return NoContent();
        }

        [HttpDelete("{brandId:Guid}")]
        [SwaggerOperation(Summary = "Delete brand")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid brandId)
        {
            var deleted = await _brandService.DeleteBrandAsync(brandId);
            if (!deleted)
                return NotFound($"Brand with ID {brandId} not found");

            return NoContent();
        }
    }
}