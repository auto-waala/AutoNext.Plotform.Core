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
    public class VehicleVariantsController : ControllerBase
    {
        private readonly IVehicleVariantService _variantService;
        private readonly ILogger<VehicleVariantsController> _logger;

        public VehicleVariantsController(
            IVehicleVariantService variantService,
            ILogger<VehicleVariantsController> logger)
        {
            _variantService = variantService;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all vehicle variants")]
        [ProducesResponseType(typeof(IEnumerable<VehicleVariantResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var variants = await _variantService.GetAllVariantsAsync();
            return Ok(variants);
        }

        [HttpGet("active")]
        [SwaggerOperation(Summary = "Get all active variants")]
        [ProducesResponseType(typeof(IEnumerable<VehicleVariantResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActive()
        {
            var variants = await _variantService.GetActiveVariantsAsync();
            return Ok(variants);
        }

        [HttpGet("available")]
        [SwaggerOperation(Summary = "Get available variants")]
        [ProducesResponseType(typeof(IEnumerable<VehicleVariantResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAvailable()
        {
            var variants = await _variantService.GetAvailableVariantsAsync();
            return Ok(variants);
        }

        [HttpGet("model/{modelId:Guid}")]
        [SwaggerOperation(Summary = "Get variants by model ID")]
        [ProducesResponseType(typeof(IEnumerable<VehicleVariantResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByModel(Guid modelId)
        {
            var variants = await _variantService.GetVariantsByModelAsync(modelId);
            return Ok(variants);
        }

        [HttpGet("{variantId:Guid}")]
        [SwaggerOperation(Summary = "Get variant by ID")]
        [ProducesResponseType(typeof(VehicleVariantResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid variantId)
        {
            var variant = await _variantService.GetVariantByIdAsync(variantId);
            if (variant == null)
                return NotFound($"Variant with ID {variantId} not found");
            return Ok(variant);
        }

        [HttpGet("code/{code}")]
        [SwaggerOperation(Summary = "Get variant by code")]
        [ProducesResponseType(typeof(VehicleVariantResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCode(string code)
        {
            var variant = await _variantService.GetVariantByCodeAsync(code);
            if (variant == null)
                return NotFound($"Variant with code {code} not found");
            return Ok(variant);
        }

        [HttpGet("{variantId:Guid}/specs")]
        [SwaggerOperation(Summary = "Get variant specifications")]
        [ProducesResponseType(typeof(VehicleVariantSpecsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSpecs(Guid variantId)
        {
            var specs = await _variantService.GetVariantSpecsAsync(variantId);
            return Ok(specs);
        }

        [HttpGet("model/{modelId:Guid}/price-range")]
        [SwaggerOperation(Summary = "Get price range for model variants")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPriceRange(Guid modelId)
        {
            var priceRange = await _variantService.GetVariantPriceRangeAsync(modelId);
            return Ok(new { modelId = modelId, priceRange = priceRange });
        }

        [HttpPost("filter")]
        [SwaggerOperation(Summary = "Filter variants")]
        [ProducesResponseType(typeof(IEnumerable<VehicleVariantResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Filter([FromBody] VehicleVariantFilterDto filterDto)
        {
            var variants = await _variantService.FilterVariantsAsync(filterDto);
            return Ok(variants);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new variant")]
        [ProducesResponseType(typeof(VehicleVariantResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] VehicleVariantCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var variant = await _variantService.CreateVariantAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { version = "1.0", variantId = variant.Id }, variant);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{variantId:Guid}")]
        [SwaggerOperation(Summary = "Update variant")]
        [ProducesResponseType(typeof(VehicleVariantResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(Guid variantId, [FromBody] VehicleVariantUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var variant = await _variantService.UpdateVariantAsync(variantId, updateDto);
                if (variant == null)
                    return NotFound($"Variant with ID {variantId} not found");

                return Ok(variant);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPatch("{variantId:Guid}/toggle/{isActive}")]
        [SwaggerOperation(Summary = "Toggle variant active status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ToggleStatus(Guid variantId, bool isActive)
        {
            var result = await _variantService.ToggleVariantStatusAsync(variantId, isActive);
            if (!result)
                return NotFound($"Variant with ID {variantId} not found");

            return NoContent();
        }

        [HttpPatch("{variantId:Guid}/availability/{isAvailable}")]
        [SwaggerOperation(Summary = "Toggle variant availability")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ToggleAvailability(Guid variantId, bool isAvailable)
        {
            var result = await _variantService.ToggleAvailabilityAsync(variantId, isAvailable);
            if (!result)
                return NotFound($"Variant with ID {variantId} not found");

            return NoContent();
        }

        [HttpPost("reorder")]
        [SwaggerOperation(Summary = "Reorder variants")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Reorder([FromBody] Dictionary<Guid, int> orderMap)
        {
            if (orderMap == null || !orderMap.Any())
                return BadRequest("Order map cannot be empty");

            await _variantService.ReorderVariantsAsync(orderMap);
            return NoContent();
        }

        [HttpDelete("{variantId:Guid}")]
        [SwaggerOperation(Summary = "Delete variant")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid variantId)
        {
            var deleted = await _variantService.DeleteVariantAsync(variantId);
            if (!deleted)
                return NotFound($"Variant with ID {variantId} not found");

            return NoContent();
        }
    }
}