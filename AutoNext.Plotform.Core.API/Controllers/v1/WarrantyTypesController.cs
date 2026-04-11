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
    public class WarrantyTypesController : ControllerBase
    {
        private readonly IWarrantyTypeService _warrantyTypeService;
        private readonly ILogger<WarrantyTypesController> _logger;

        public WarrantyTypesController(
            IWarrantyTypeService warrantyTypeService,
            ILogger<WarrantyTypesController> logger)
        {
            _warrantyTypeService = warrantyTypeService;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all warranty types")]
        [ProducesResponseType(typeof(IEnumerable<WarrantyTypeResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var warrantyTypes = await _warrantyTypeService.GetAllWarrantyTypesAsync();
            return Ok(warrantyTypes);
        }

        [HttpGet("active")]
        [SwaggerOperation(Summary = "Get all active warranty types")]
        [ProducesResponseType(typeof(IEnumerable<WarrantyTypeResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActive()
        {
            var warrantyTypes = await _warrantyTypeService.GetActiveWarrantyTypesAsync();
            return Ok(warrantyTypes);
        }

        [HttpGet("transferable")]
        [SwaggerOperation(Summary = "Get transferable warranty types")]
        [ProducesResponseType(typeof(IEnumerable<WarrantyTypeResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransferable()
        {
            var warrantyTypes = await _warrantyTypeService.GetTransferableWarrantyTypesAsync();
            return Ok(warrantyTypes);
        }

        [HttpGet("{warrantyTypeId:Guid}")]
        [SwaggerOperation(Summary = "Get warranty type by ID")]
        [ProducesResponseType(typeof(WarrantyTypeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid warrantyTypeId)
        {
            var warrantyType = await _warrantyTypeService.GetWarrantyTypeByIdAsync(warrantyTypeId);
            if (warrantyType == null)
                return NotFound($"Warranty type with ID {warrantyTypeId} not found");
            return Ok(warrantyType);
        }

        [HttpGet("code/{code}")]
        [SwaggerOperation(Summary = "Get warranty type by code")]
        [ProducesResponseType(typeof(WarrantyTypeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCode(string code)
        {
            var warrantyType = await _warrantyTypeService.GetWarrantyTypeByCodeAsync(code);
            if (warrantyType == null)
                return NotFound($"Warranty type with code {code} not found");
            return Ok(warrantyType);
        }

        [HttpGet("category/{categoryCode}")]
        [SwaggerOperation(Summary = "Get warranty types by category")]
        [ProducesResponseType(typeof(IEnumerable<WarrantyTypeResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCategory(string categoryCode)
        {
            var warrantyTypes = await _warrantyTypeService.GetWarrantyTypesByCategoryAsync(categoryCode);
            return Ok(warrantyTypes);
        }

        [HttpGet("category/{categoryCode}/best")]
        [SwaggerOperation(Summary = "Get best warranty by category")]
        [ProducesResponseType(typeof(WarrantyTypeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBestByCategory(string categoryCode)
        {
            var warranty = await _warrantyTypeService.GetBestWarrantyByCategoryAsync(categoryCode);
            if (warranty == null)
                return NotFound($"No warranty types found for category {categoryCode}");
            return Ok(warranty);
        }

        [HttpPost("filter")]
        [SwaggerOperation(Summary = "Filter warranty types")]
        [ProducesResponseType(typeof(IEnumerable<WarrantyTypeResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Filter([FromBody] WarrantyTypeFilterDto filterDto)
        {
            var warrantyTypes = await _warrantyTypeService.FilterWarrantyTypesAsync(filterDto);
            return Ok(warrantyTypes);
        }

        [HttpGet("compare/{warrantyIdA:Guid}/{warrantyIdB:Guid}")]
        [SwaggerOperation(Summary = "Compare two warranty types")]
        [ProducesResponseType(typeof(WarrantyComparisonDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Compare(Guid warrantyIdA, Guid warrantyIdB)
        {
            try
            {
                var comparison = await _warrantyTypeService.CompareWarrantiesAsync(warrantyIdA, warrantyIdB);
                return Ok(comparison);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new warranty type")]
        [ProducesResponseType(typeof(WarrantyTypeResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] WarrantyTypeCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var warrantyType = await _warrantyTypeService.CreateWarrantyTypeAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { version = "1.0", warrantyTypeId = warrantyType.Id }, warrantyType);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{warrantyTypeId:Guid}")]
        [SwaggerOperation(Summary = "Update warranty type")]
        [ProducesResponseType(typeof(WarrantyTypeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(Guid warrantyTypeId, [FromBody] WarrantyTypeUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var warrantyType = await _warrantyTypeService.UpdateWarrantyTypeAsync(warrantyTypeId, updateDto);
                if (warrantyType == null)
                    return NotFound($"Warranty type with ID {warrantyTypeId} not found");

                return Ok(warrantyType);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{warrantyTypeId:Guid}/toggle/{isActive}")]
        [SwaggerOperation(Summary = "Toggle warranty type active status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ToggleStatus(Guid warrantyTypeId, bool isActive)
        {
            var result = await _warrantyTypeService.ToggleWarrantyTypeStatusAsync(warrantyTypeId, isActive);
            if (!result)
                return NotFound($"Warranty type with ID {warrantyTypeId} not found");

            return NoContent();
        }

        [HttpPost("reorder")]
        [SwaggerOperation(Summary = "Reorder warranty types")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Reorder([FromBody] Dictionary<Guid, int> orderMap)
        {
            if (orderMap == null || !orderMap.Any())
                return BadRequest("Order map cannot be empty");

            await _warrantyTypeService.ReorderWarrantyTypesAsync(orderMap);
            return NoContent();
        }

        [HttpDelete("{warrantyTypeId:Guid}")]
        [SwaggerOperation(Summary = "Delete warranty type")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid warrantyTypeId)
        {
            var deleted = await _warrantyTypeService.DeleteWarrantyTypeAsync(warrantyTypeId);
            if (!deleted)
                return NotFound($"Warranty type with ID {warrantyTypeId} not found");

            return NoContent();
        }
    }
}