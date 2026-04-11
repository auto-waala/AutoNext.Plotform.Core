using Asp.Versioning;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoNext.Plotform.Core.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/service-types")]
    public class ServiceTypesController : ControllerBase
    {
        private readonly IServiceTypeService _service;

        public ServiceTypesController(IServiceTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("active")]
        public async Task<IActionResult> GetActive() =>
            Ok(await _service.GetActiveAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceTypeCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ServiceTypeUpdateDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPatch("{id}/toggle/{isActive}")]
        public async Task<IActionResult> Toggle(Guid id, bool isActive)
        {
            var success = await _service.ToggleStatusAsync(id, isActive);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}