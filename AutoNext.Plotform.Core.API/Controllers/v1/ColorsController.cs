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
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _colorService;
        private readonly ILogger<ColorsController> _logger;

        public ColorsController(
            IColorService colorService,
            ILogger<ColorsController> logger)
        {
            _colorService = colorService;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all colors")]
        [ProducesResponseType(typeof(IEnumerable<ColorResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var colors = await _colorService.GetAllColorsAsync();
            return Ok(colors);
        }

        [HttpGet("active")]
        [SwaggerOperation(Summary = "Get all active colors")]
        [ProducesResponseType(typeof(IEnumerable<ColorResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActive()
        {
            var colors = await _colorService.GetActiveColorsAsync();
            return Ok(colors);
        }

        [HttpGet("ordered")]
        [SwaggerOperation(Summary = "Get colors ordered by display order")]
        [ProducesResponseType(typeof(IEnumerable<ColorResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrdered()
        {
            var colors = await _colorService.GetColorsByDisplayOrderAsync();
            return Ok(colors);
        }

        [HttpGet("{colorId:Guid}")]
        [SwaggerOperation(Summary = "Get color by ID")]
        [ProducesResponseType(typeof(ColorResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid colorId)
        {
            var color = await _colorService.GetColorByIdAsync(colorId);
            if (color == null)
                return NotFound($"Color with ID {colorId} not found");
            return Ok(color);
        }

        [HttpGet("code/{code}")]
        [SwaggerOperation(Summary = "Get color by code")]
        [ProducesResponseType(typeof(ColorResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCode(string code)
        {
            var color = await _colorService.GetColorByCodeAsync(code);
            if (color == null)
                return NotFound($"Color with code {code} not found");
            return Ok(color);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new color")]
        [ProducesResponseType(typeof(ColorResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] ColorCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var color = await _colorService.CreateColorAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { version = "1.0", colorId = color.Id }, color);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{colorId:Guid}")]
        [SwaggerOperation(Summary = "Update color")]
        [ProducesResponseType(typeof(ColorResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(Guid colorId, [FromBody] ColorUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var color = await _colorService.UpdateColorAsync(colorId, updateDto);
                if (color == null)
                    return NotFound($"Color with ID {colorId} not found");

                return Ok(color);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPatch("{colorId:Guid}/toggle/{isActive}")]
        [SwaggerOperation(Summary = "Toggle color active status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ToggleStatus(Guid colorId, bool isActive)
        {
            var result = await _colorService.ToggleColorStatusAsync(colorId, isActive);
            if (!result)
                return NotFound($"Color with ID {colorId} not found");

            return NoContent();
        }

        [HttpPost("reorder")]
        [SwaggerOperation(Summary = "Reorder colors")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Reorder([FromBody] Dictionary<Guid, int> orderMap)
        {
            if (orderMap == null || !orderMap.Any())
                return BadRequest("Order map cannot be empty");

            await _colorService.ReorderColorsAsync(orderMap);
            return NoContent();
        }

        [HttpGet("validate-hex")]
        [SwaggerOperation(Summary = "Validate hex color code")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateHexCode([FromQuery] string hexCode)
        {
            var isValid = await _colorService.ValidateHexCodeAsync(hexCode);
            return Ok(new { hexCode = hexCode, isValid = isValid });
        }

        [HttpGet("convert-hex-to-rgb")]
        [SwaggerOperation(Summary = "Convert hex color to RGB")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConvertHexToRgb([FromQuery] string hexCode)
        {
            if (string.IsNullOrEmpty(hexCode))
                return BadRequest("Hex code is required");

            var rgbValue = await _colorService.ConvertHexToRgbAsync(hexCode);
            if (rgbValue == null)
                return BadRequest("Invalid hex code format");

            return Ok(new { hexCode = hexCode, rgbValue = rgbValue });
        }

        [HttpDelete("{colorId:Guid}")]
        [SwaggerOperation(Summary = "Delete color")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid colorId)
        {
            var deleted = await _colorService.DeleteColorAsync(colorId);
            if (!deleted)
                return NotFound($"Color with ID {colorId} not found");

            return NoContent();
        }
    }
}