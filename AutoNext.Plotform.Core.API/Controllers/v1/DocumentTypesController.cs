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
    public class DocumentTypesController : ControllerBase
    {
        private readonly IDocumentTypeService _documentTypeService;
        private readonly ILogger<DocumentTypesController> _logger;

        public DocumentTypesController(
            IDocumentTypeService documentTypeService,
            ILogger<DocumentTypesController> logger)
        {
            _documentTypeService = documentTypeService;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all document types")]
        [ProducesResponseType(typeof(IEnumerable<DocumentTypeResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var documentTypes = await _documentTypeService.GetAllDocumentTypesAsync();
            return Ok(documentTypes);
        }

        [HttpGet("active")]
        [SwaggerOperation(Summary = "Get all active document types")]
        [ProducesResponseType(typeof(IEnumerable<DocumentTypeResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActive()
        {
            var documentTypes = await _documentTypeService.GetActiveDocumentTypesAsync();
            return Ok(documentTypes);
        }

        [HttpGet("required")]
        [SwaggerOperation(Summary = "Get required document types")]
        [ProducesResponseType(typeof(IEnumerable<DocumentTypeResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRequired()
        {
            var documentTypes = await _documentTypeService.GetRequiredDocumentTypesAsync();
            return Ok(documentTypes);
        }

        [HttpGet("verifiable")]
        [SwaggerOperation(Summary = "Get verifiable document types")]
        [ProducesResponseType(typeof(IEnumerable<DocumentTypeResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVerifiable()
        {
            var documentTypes = await _documentTypeService.GetVerifiableDocumentTypesAsync();
            return Ok(documentTypes);
        }

        [HttpGet("categories")]
        [SwaggerOperation(Summary = "Get distinct categories")]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _documentTypeService.GetDistinctCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("grouped")]
        [SwaggerOperation(Summary = "Get document types grouped by category")]
        [ProducesResponseType(typeof(IEnumerable<DocumentTypeCategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGrouped()
        {
            var grouped = await _documentTypeService.GetDocumentTypesGroupedByCategoryAsync();
            return Ok(grouped);
        }

        [HttpGet("{documentTypeId:Guid}")]
        [SwaggerOperation(Summary = "Get document type by ID")]
        [ProducesResponseType(typeof(DocumentTypeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid documentTypeId)
        {
            var documentType = await _documentTypeService.GetDocumentTypeByIdAsync(documentTypeId);
            if (documentType == null)
                return NotFound($"Document type with ID {documentTypeId} not found");
            return Ok(documentType);
        }

        [HttpGet("code/{code}")]
        [SwaggerOperation(Summary = "Get document type by code")]
        [ProducesResponseType(typeof(DocumentTypeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCode(string code)
        {
            var documentType = await _documentTypeService.GetDocumentTypeByCodeAsync(code);
            if (documentType == null)
                return NotFound($"Document type with code {code} not found");
            return Ok(documentType);
        }

        [HttpGet("category/{category}")]
        [SwaggerOperation(Summary = "Get document types by category")]
        [ProducesResponseType(typeof(IEnumerable<DocumentTypeResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var documentTypes = await _documentTypeService.GetDocumentTypesByCategoryAsync(category);
            return Ok(documentTypes);
        }

        [HttpGet("vehicle-type/{vehicleType}")]
        [SwaggerOperation(Summary = "Get document types by vehicle type")]
        [ProducesResponseType(typeof(IEnumerable<DocumentTypeResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByVehicleType(string vehicleType)
        {
            var documentTypes = await _documentTypeService.GetDocumentTypesByVehicleTypeAsync(vehicleType);
            return Ok(documentTypes);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new document type")]
        [ProducesResponseType(typeof(DocumentTypeResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] DocumentTypeCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var documentType = await _documentTypeService.CreateDocumentTypeAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { version = "1.0", documentTypeId = documentType.Id }, documentType);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{documentTypeId:Guid}")]
        [SwaggerOperation(Summary = "Update document type")]
        [ProducesResponseType(typeof(DocumentTypeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(Guid documentTypeId, [FromBody] DocumentTypeUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var documentType = await _documentTypeService.UpdateDocumentTypeAsync(documentTypeId, updateDto);
                if (documentType == null)
                    return NotFound($"Document type with ID {documentTypeId} not found");

                return Ok(documentType);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPatch("{documentTypeId:Guid}/toggle/{isActive}")]
        [SwaggerOperation(Summary = "Toggle document type active status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ToggleStatus(Guid documentTypeId, bool isActive)
        {
            var result = await _documentTypeService.ToggleDocumentTypeStatusAsync(documentTypeId, isActive);
            if (!result)
                return NotFound($"Document type with ID {documentTypeId} not found");

            return NoContent();
        }

        [HttpPost("reorder")]
        [SwaggerOperation(Summary = "Reorder document types")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Reorder([FromBody] Dictionary<Guid, int> orderMap)
        {
            if (orderMap == null || !orderMap.Any())
                return BadRequest("Order map cannot be empty");

            await _documentTypeService.ReorderDocumentTypesAsync(orderMap);
            return NoContent();
        }

        [HttpDelete("{documentTypeId:Guid}")]
        [SwaggerOperation(Summary = "Delete document type")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid documentTypeId)
        {
            var deleted = await _documentTypeService.DeleteDocumentTypeAsync(documentTypeId);
            if (!deleted)
                return NotFound($"Document type with ID {documentTypeId} not found");

            return NoContent();
        }
    }
}