using Asp.Versioning;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoNext.Plotform.Core.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FuelTypesController : ControllerBase
    {
        private readonly IFuelTypeService _fuelTypeService;
        private readonly ILogger<FuelTypesController> _logger;

        public FuelTypesController(
            IFuelTypeService fuelTypeService,
            ILogger<FuelTypesController> logger)
        {
            _fuelTypeService = fuelTypeService;
            _logger = logger;
        }

        /// <summary>
        /// Get all fuel types
        /// </summary>
        /// <param name="onlyActive">If true, returns only active fuel types</param>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool onlyActive = false)
        {
            var fuelTypes = await _fuelTypeService.GetAllAsync(onlyActive);
            return Ok(fuelTypes);
        }

        /// <summary>
        /// Get fuel type by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var fuelType = await _fuelTypeService.GetByIdAsync(id);
            if (fuelType == null)
                return NotFound(new { message = $"Fuel type with ID {id} not found" });

            return Ok(fuelType);
        }

        /// <summary>
        /// Get fuel type by code
        /// </summary>
        [HttpGet("by-code/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var allFuelTypes = await _fuelTypeService.GetAllAsync(false);
            var fuelType = allFuelTypes.FirstOrDefault(f => f.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

            if (fuelType == null)
                return NotFound(new { message = $"Fuel type with code {code} not found" });

            return Ok(fuelType);
        }

        /// <summary>
        /// Create a new fuel type
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FuelTypeCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var fuelType = await _fuelTypeService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = fuelType.Id }, fuelType);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating fuel type");
                return StatusCode(500, new { message = "An error occurred while creating the fuel type" });
            }
        }

        /// <summary>
        /// Update an existing fuel type
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] FuelTypeUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var fuelType = await _fuelTypeService.UpdateAsync(updateDto);
                if (fuelType == null)
                    return NotFound(new { message = $"Fuel type with ID {updateDto.Id} not found" });

                return Ok(fuelType);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating fuel type with ID: {Id}", updateDto.Id);
                return StatusCode(500, new { message = "An error occurred while updating the fuel type" });
            }
        }

        /// <summary>
        /// Delete a fuel type
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _fuelTypeService.DeleteAsync(id);
                if (!deleted)
                    return NotFound(new { message = $"Fuel type with ID {id} not found" });

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting fuel type with ID: {Id}", id);
                return StatusCode(500, new { message = "An error occurred while deleting the fuel type" });
            }
        }

        /// <summary>
        /// Toggle active status of a fuel type
        /// </summary>
        [HttpPatch("{id}/toggle-active")]
        public async Task<IActionResult> ToggleActive(Guid id)
        {
            try
            {
                var toggled = await _fuelTypeService.ToggleActiveAsync(id);
                if (!toggled)
                    return NotFound(new { message = $"Fuel type with ID {id} not found" });

                return Ok(new { message = "Fuel type active status toggled successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling active status for fuel type ID: {Id}", id);
                return StatusCode(500, new { message = "An error occurred while toggling active status" });
            }
        }

        /// <summary>
        /// Bulk create fuel types
        /// </summary>
        [HttpPost("bulk")]
        public async Task<IActionResult> BulkCreate([FromBody] List<FuelTypeCreateDto> createDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var results = new List<FuelTypeResponseDto>();
            var errors = new List<string>();

            foreach (var createDto in createDtos)
            {
                try
                {
                    var fuelType = await _fuelTypeService.CreateAsync(createDto);
                    results.Add(fuelType);
                }
                catch (Exception ex)
                {
                    errors.Add($"Failed to create {createDto.Name}: {ex.Message}");
                }
            }

            return Ok(new { success = results, errors = errors });
        }
    }
}