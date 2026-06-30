using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;
using AutoNext.Plotform.Core.API.Services;

namespace AutoNext.Platform.Core.API.Services
{
    public class EnquiryService : IEnquiryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EnquiryService> _logger;

        public EnquiryService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<EnquiryService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<EnquiryResponseDto>> GetAllAsync()
        {
            var enquiries = await _unitOfWork.Enquiries.GetAllAsync();
            return enquiries.Select(e => _mapper.Map<EnquiryResponseDto>(e));
        }

        public async Task<EnquiryResponseDto?> GetByIdAsync(Guid id)
        {
            try
            {
                var enquiry = await _unitOfWork.Enquiries.GetByIdAsync(id);

                if (enquiry == null)
                    return null;

                return _mapper.Map<EnquiryResponseDto>(enquiry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting enquiry by ID: {Id}", id);
                throw;
            }
        }

        public async Task<EnquiryResponseDto> CreateAsync(EnquiryCreateDto createDto)
        {

            try
            {
                var enquiry = _mapper.Map<Enquiry>(createDto);

                await _unitOfWork.Enquiries.AddAsync(enquiry);

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Created enquiry: {Name}", enquiry.Name);

                return _mapper.Map<EnquiryResponseDto>(enquiry);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();

                _logger.LogError(ex, "Error creating enquiry");

                throw;
            }
        }
    }
}
