using Asp.Versioning;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoNext.Plotform.Core.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EnquiryController : ControllerBase
    {
        private readonly IEnquiryService _enquiryService;
        private readonly ILogger<EnquiryController> _logger;

        public EnquiryController(
            IEnquiryService enquiryService,
            ILogger<EnquiryController> logger)
        {
            _enquiryService = enquiryService;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all enquiries")]
        [ProducesResponseType(typeof(IEnumerable<EnquiryResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var enquiries = await _enquiryService.GetAllAsync();
            return Ok(enquiries);
        }

        [HttpGet("{id:Guid}")]
        [SwaggerOperation(Summary = "Get enquiry by ID")]
        [ProducesResponseType(typeof(EnquiryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var enquiry = await _enquiryService.GetByIdAsync(id);

            if (enquiry == null)
                return NotFound($"Enquiry with ID {id} not found");

            return Ok(enquiry);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new enquiry")]
        [ProducesResponseType(typeof(EnquiryResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] EnquiryCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var enquiry = await _enquiryService.CreateAsync(createDto);

            return CreatedAtAction(
                nameof(GetById),
                new { version = "1.0", id = enquiry.Id },
                enquiry);
        }
    }
}
