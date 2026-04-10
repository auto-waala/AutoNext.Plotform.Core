using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoNext.Plotform.Core.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly ILogger<LocationsController> _logger;

        public LocationsController(
            ILocationService locationService,
            ILogger<LocationsController> logger)
        {
            _locationService = locationService;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all locations")]
        [ProducesResponseType(typeof(IEnumerable<LocationResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var locations = await _locationService.GetAllLocationsAsync();
            return Ok(locations);
        }

        [HttpGet("{locationid:Guid}")]
        [SwaggerOperation(Summary = "Get location by ID")]
        [ProducesResponseType(typeof(LocationResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid locationid)
        {
            var location = await _locationService.GetLocationByIdAsync(locationid);
            if (location == null)
                return NotFound($"Location with ID {locationid} not found");
            return Ok(location);
        }

        [HttpGet("state/{stateCode}")]
        [SwaggerOperation(Summary = "Get locations by state code")]
        public async Task<IActionResult> GetByState(string stateCode)
        {
            var locations = await _locationService.GetLocationsByStateAsync(stateCode);
            return Ok(locations);
        }

        [HttpGet("city/{cityName}")]
        [SwaggerOperation(Summary = "Search locations by city name")]
        public async Task<IActionResult> GetByCity(string cityName)
        {
            var locations = await _locationService.GetLocationsByCityAsync(cityName);
            return Ok(locations);
        }

        [HttpGet("{locationId:Guid}/areas")]
        [SwaggerOperation(Summary = "Get areas under a location")]
        public async Task<IActionResult> GetAreas(Guid locationId)
        {
            var areas = await _locationService.GetAreasByCityAsync(locationId);
            return Ok(areas);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new location")]
        [ProducesResponseType(typeof(LocationResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] LocationCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var location = await _locationService.CreateLocationAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = location.Id }, location);
        }

        [HttpDelete("{id:Guid}")]
        [SwaggerOperation(Summary = "Delete location")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _locationService.DeleteLocationAsync(id);
            if (!deleted)
                return NotFound($"Location with ID {id} not found");
            return NoContent();
        }
    }
}
