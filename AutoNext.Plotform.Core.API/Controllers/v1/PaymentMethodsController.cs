using Asp.Versioning;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoNext.Plotform.Core.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/payment-methods")]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IPaymentMethodService _service;

        public PaymentMethodsController(IPaymentMethodService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all payment methods")]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("active")]
        [SwaggerOperation(Summary = "Get active payment methods")]
        public async Task<IActionResult> GetActive() =>
            Ok(await _service.GetActiveAsync());

        [HttpGet("{id:Guid}")]
        [SwaggerOperation(Summary = "Get payment method by ID")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("type/{type}")]
        [SwaggerOperation(Summary = "Get payment methods by type")]
        public async Task<IActionResult> GetByType(string type) =>
            Ok(await _service.GetByTypeAsync(type));

        [HttpPost]
        [SwaggerOperation(Summary = "Create payment method")]
        public async Task<IActionResult> Create([FromBody] PaymentMethodCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),
                new { version = "1.0", id = result.Id },
                result);
        }

        [HttpPut("{id:Guid}")]
        [SwaggerOperation(Summary = "Update payment method")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PaymentMethodUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.UpdateAsync(id, dto);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPatch("{id:Guid}/toggle/{isActive}")]
        [SwaggerOperation(Summary = "Toggle payment method status")]
        public async Task<IActionResult> Toggle(Guid id, bool isActive)
        {
            var success = await _service.ToggleStatusAsync(id, isActive);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id:Guid}")]
        [SwaggerOperation(Summary = "Delete payment method")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}