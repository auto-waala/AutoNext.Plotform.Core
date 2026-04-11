using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;

namespace AutoNext.Plotform.Core.API.Services
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<VehicleTypeService> _logger;

        public VehicleTypeService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<VehicleTypeService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<VehicleTypeResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.VehicleTypes.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<VehicleTypeResponseDto>(entity);
        }

        public async Task<IEnumerable<VehicleTypeResponseDto>> GetAllAsync(bool onlyActive = false)
        {
            IEnumerable<VehicleType> entities;
            if (onlyActive)
                entities = await _unitOfWork.VehicleTypes.FindAsync(v => v.IsActive);
            else
                entities = await _unitOfWork.VehicleTypes.GetAllAsync();

            return _mapper.Map<IEnumerable<VehicleTypeResponseDto>>(
                entities.OrderBy(v => v.SortOrder).ThenBy(v => v.Name));
        }

        public async Task<VehicleTypeResponseDto> CreateAsync(VehicleTypeCreateDto createDto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var entity = _mapper.Map<VehicleType>(createDto);
                entity.Id = Guid.NewGuid();
                entity.CreatedAt = DateTime.UtcNow;

                await _unitOfWork.VehicleTypes.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Created vehicle type: {Name} with code: {Code}",
                    entity.Name, entity.Code);

                return _mapper.Map<VehicleTypeResponseDto>(entity);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error creating vehicle type");
                throw;
            }
        }

        public async Task<VehicleTypeResponseDto?> UpdateAsync(VehicleTypeUpdateDto updateDto)
        {
            var entity = await _unitOfWork.VehicleTypes.GetByIdAsync(updateDto.Id);
            if (entity == null)
                return null;

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                _mapper.Map(updateDto, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.VehicleTypes.Update(entity);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Updated vehicle type: {Name} with code: {Code}",
                    entity.Name, entity.Code);

                return _mapper.Map<VehicleTypeResponseDto>(entity);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error updating vehicle type with ID: {Id}", updateDto.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _unitOfWork.VehicleTypes.GetByIdAsync(id);
            if (entity == null)
                return false;

            _unitOfWork.VehicleTypes.Remove(entity);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Deleted vehicle type with ID: {Id}", id);
            return true;
        }

        public async Task<bool> ToggleActiveAsync(Guid id)
        {
            var entity = await _unitOfWork.VehicleTypes.GetByIdAsync(id);
            if (entity == null)
                return false;

            entity.IsActive = !entity.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.VehicleTypes.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Toggled active status for vehicle type ID: {Id} to {Status}",
                id, entity.IsActive);

            return true;
        }
    }
}
