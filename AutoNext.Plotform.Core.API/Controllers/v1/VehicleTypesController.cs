using Asp.Versioning;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoNext.Plotform.Core.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VehicleTypesController : ControllerBase
    {
        private readonly IVehicleTypeService _vehicleTypeService;
        private readonly ILogger<VehicleTypesController> _logger;

        public VehicleTypesController(
            IVehicleTypeService vehicleTypeService,
            ILogger<VehicleTypesController> logger)
        {
            _vehicleTypeService = vehicleTypeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool onlyActive = false)
        {
            _logger.LogInformation(
                "Fetching all VehicleTypes. OnlyActive:{OnlyActive}",
                onlyActive);

            var types = await _vehicleTypeService.GetAllAsync(onlyActive);

            _logger.LogInformation(
                "Retrieved {Count} VehicleTypes",
                types.Count());

            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation(
                "Fetching VehicleType with Id:{Id}",
                id);

            var type = await _vehicleTypeService.GetByIdAsync(id);

            if (type == null)
            {
                _logger.LogWarning(
                    "VehicleType not found for Id:{Id}",
                    id);

                return NotFound();
            }

            _logger.LogInformation(
                "VehicleType retrieved successfully for Id:{Id}",
                id);

            return Ok(type);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] VehicleTypeCreateDto createDto)
        {
            _logger.LogInformation(
                "Creating VehicleType: {Name}",
                createDto.Name);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning(
                    "Invalid request for VehicleType creation");

                return BadRequest(ModelState);
            }

            var type = await _vehicleTypeService.CreateAsync(createDto);

            _logger.LogInformation(
                "VehicleType created successfully with Id:{Id}",
                type.Id);

            return CreatedAtAction(
                nameof(GetById),
                new { id = type.Id },
                type);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] VehicleTypeUpdateDto updateDto)
        {
            _logger.LogInformation(
                "Updating VehicleType Id:{Id}",
                updateDto.Id);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning(
                    "Invalid update request for VehicleType Id:{Id}",
                    updateDto.Id);

                return BadRequest(ModelState);
            }

            var type = await _vehicleTypeService.UpdateAsync(updateDto);

            if (type == null)
            {
                _logger.LogWarning(
                    "VehicleType not found during update Id:{Id}",
                    updateDto.Id);

                return NotFound();
            }

            _logger.LogInformation(
                "VehicleType updated successfully Id:{Id}",
                updateDto.Id);

            return Ok(type);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation(
                "Deleting VehicleType Id:{Id}",
                id);

            var deleted = await _vehicleTypeService.DeleteAsync(id);

            if (!deleted)
            {
                _logger.LogWarning(
                    "VehicleType not found during delete Id:{Id}",
                    id);

                return NotFound();
            }

            _logger.LogInformation(
                "VehicleType deleted successfully Id:{Id}",
                id);

            return NoContent();
        }

        [HttpPatch("{id}/toggle-active")]
        public async Task<IActionResult> ToggleActive(Guid id)
        {
            _logger.LogInformation(
                "Toggling active status for VehicleType Id:{Id}",
                id);

            var toggled = await _vehicleTypeService.ToggleActiveAsync(id);

            if (!toggled)
            {
                _logger.LogWarning(
                    "VehicleType not found while toggling status Id:{Id}",
                    id);

                return NotFound();
            }

            _logger.LogInformation(
                "VehicleType status toggled successfully Id:{Id}",
                id);

            return Ok();
        }
    }
}