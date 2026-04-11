using Asp.Versioning;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoNext.Plotform.Core.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TransmissionsController : ControllerBase
    {
        private readonly ITransmissionService _transmissionService;
        private readonly ILogger<TransmissionsController> _logger;

        public TransmissionsController(
            ITransmissionService transmissionService,
            ILogger<TransmissionsController> logger)
        {
            _transmissionService = transmissionService;
            _logger = logger;
        }

        /// <summary>
        /// Get all transmissions
        /// </summary>
        /// <param name="onlyActive">If true, returns only active transmissions</param>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool onlyActive = false)
        {
            var transmissions = await _transmissionService.GetAllAsync(onlyActive);
            return Ok(transmissions);
        }

        /// <summary>
        /// Get transmission by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var transmission = await _transmissionService.GetByIdAsync(id);
            if (transmission == null)
                return NotFound(new { message = $"Transmission with ID {id} not found" });

            return Ok(transmission);
        }

        /// <summary>
        /// Get transmission by code
        /// </summary>
        [HttpGet("by-code/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var allTransmissions = await _transmissionService.GetAllAsync(false);
            var transmission = allTransmissions.FirstOrDefault(t => t.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

            if (transmission == null)
                return NotFound(new { message = $"Transmission with code {code} not found" });

            return Ok(transmission);
        }

        /// <summary>
        /// Get transmissions by number of gears
        /// </summary>
        [HttpGet("by-gears/{gearsCount}")]
        public async Task<IActionResult> GetByGearsCount(int gearsCount)
        {
            var transmissions = await _transmissionService.GetByGearsCountAsync(gearsCount);
            return Ok(transmissions);
        }

        /// <summary>
        /// Get automatic transmissions
        /// </summary>
        [HttpGet("automatic")]
        public async Task<IActionResult> GetAutomaticTransmissions()
        {
            var allTransmissions = await _transmissionService.GetAllAsync(true);
            var automatic = allTransmissions.Where(t =>
                t.Name.Contains("Automatic", StringComparison.OrdinalIgnoreCase) ||
                t.Code.Contains("AT", StringComparison.OrdinalIgnoreCase));

            return Ok(automatic);
        }

        /// <summary>
        /// Get manual transmissions
        /// </summary>
        [HttpGet("manual")]
        public async Task<IActionResult> GetManualTransmissions()
        {
            var allTransmissions = await _transmissionService.GetAllAsync(true);
            var manual = allTransmissions.Where(t =>
                t.Name.Contains("Manual", StringComparison.OrdinalIgnoreCase) ||
                t.Code.Contains("MT", StringComparison.OrdinalIgnoreCase));

            return Ok(manual);
        }

        /// <summary>
        /// Create a new transmission
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransmissionCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var transmission = await _transmissionService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = transmission.Id }, transmission);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating transmission");
                return StatusCode(500, new { message = "An error occurred while creating the transmission" });
            }
        }

        /// <summary>
        /// Update an existing transmission
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TransmissionUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var transmission = await _transmissionService.UpdateAsync(updateDto);
                if (transmission == null)
                    return NotFound(new { message = $"Transmission with ID {updateDto.Id} not found" });

                return Ok(transmission);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating transmission with ID: {Id}", updateDto.Id);
                return StatusCode(500, new { message = "An error occurred while updating the transmission" });
            }
        }

        /// <summary>
        /// Delete a transmission
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _transmissionService.DeleteAsync(id);
                if (!deleted)
                    return NotFound(new { message = $"Transmission with ID {id} not found" });

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting transmission with ID: {Id}", id);
                return StatusCode(500, new { message = "An error occurred while deleting the transmission" });
            }
        }

        /// <summary>
        /// Toggle active status of a transmission
        /// </summary>
        [HttpPatch("{id}/toggle-active")]
        public async Task<IActionResult> ToggleActive(Guid id)
        {
            try
            {
                var toggled = await _transmissionService.ToggleActiveAsync(id);
                if (!toggled)
                    return NotFound(new { message = $"Transmission with ID {id} not found" });

                return Ok(new { message = "Transmission active status toggled successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling active status for transmission ID: {Id}", id);
                return StatusCode(500, new { message = "An error occurred while toggling active status" });
            }
        }

        /// <summary>
        /// Bulk create transmissions
        /// </summary>
        [HttpPost("bulk")]
        public async Task<IActionResult> BulkCreate([FromBody] List<TransmissionCreateDto> createDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var results = new List<TransmissionResponseDto>();
            var errors = new List<string>();

            foreach (var createDto in createDtos)
            {
                try
                {
                    var transmission = await _transmissionService.CreateAsync(createDto);
                    results.Add(transmission);
                }
                catch (Exception ex)
                {
                    errors.Add($"Failed to create {createDto.Name}: {ex.Message}");
                }
            }

            return Ok(new { success = results, errors = errors });
        }

        /// <summary>
        /// Get transmission statistics
        /// </summary>
        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var allTransmissions = await _transmissionService.GetAllAsync(false);

            var statistics = new
            {
                TotalCount = allTransmissions.Count(),
                ActiveCount = allTransmissions.Count(t => t.IsActive),
                InactiveCount = allTransmissions.Count(t => !t.IsActive),
                ByGearsCount = allTransmissions
                    .GroupBy(t => t.GearsCount)
                    .Select(g => new { GearsCount = g.Key, Count = g.Count() }),
                AverageGearsCount = allTransmissions
                    .Where(t => t.GearsCount.HasValue)
                    .Average(t => t.GearsCount)
            };

            return Ok(statistics);
        }
    }
}