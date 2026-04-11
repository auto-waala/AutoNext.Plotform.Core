using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;

namespace AutoNext.Plotform.Core.API.Services
{
    public class FuelTypeService : IFuelTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<FuelTypeService> _logger;

        public FuelTypeService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<FuelTypeService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<FuelTypeResponseDto> CreateAsync(FuelTypeCreateDto createDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FuelTypeResponseDto>> GetAllAsync(bool onlyActive = false)
        {
            throw new NotImplementedException();
        }

        public Task<FuelTypeResponseDto?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ToggleActiveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<FuelTypeResponseDto?> UpdateAsync(FuelTypeUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
