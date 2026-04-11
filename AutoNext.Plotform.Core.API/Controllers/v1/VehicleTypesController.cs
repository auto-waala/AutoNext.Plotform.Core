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
            var types = await _vehicleTypeService.GetAllAsync(onlyActive);
            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var type = await _vehicleTypeService.GetByIdAsync(id);
            if (type == null)
                return NotFound();

            return Ok(type);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VehicleTypeCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var type = await _vehicleTypeService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = type.Id }, type);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] VehicleTypeUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var type = await _vehicleTypeService.UpdateAsync(updateDto);
            if (type == null)
                return NotFound();

            return Ok(type);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _vehicleTypeService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id}/toggle-active")]
        public async Task<IActionResult> ToggleActive(Guid id)
        {
            var toggled = await _vehicleTypeService.ToggleActiveAsync(id);
            if (!toggled)
                return NotFound();

            return Ok();
        }
    }
}