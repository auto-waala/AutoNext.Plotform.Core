using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;

namespace AutoNext.Plotform.Core.API.Services
{
    public class TransmissionService: ITransmissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TransmissionService> _logger;

        public TransmissionService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<TransmissionService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<TransmissionResponseDto> CreateAsync(TransmissionCreateDto createDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TransmissionResponseDto>> GetAllAsync(bool onlyActive = false)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TransmissionResponseDto>> GetByGearsCountAsync(int gearsCount)
        {
            var entities = await _unitOfWork.Transmissions
                .FindAsync(t => t.GearsCount == gearsCount && t.IsActive);
            return _mapper.Map<IEnumerable<TransmissionResponseDto>>(entities);
        }

        public Task<TransmissionResponseDto?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ToggleActiveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TransmissionResponseDto?> UpdateAsync(TransmissionUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
