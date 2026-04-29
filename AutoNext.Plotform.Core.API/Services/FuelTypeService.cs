using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;

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

        public async Task<FuelTypeResponseDto> CreateAsync(FuelTypeCreateDto createDto)
        {
            try
            {
                var entity = _mapper.Map<FuelType>(createDto);

                entity.Id = Guid.NewGuid();
                entity.IsActive = true;
                entity.CreatedAt = DateTime.UtcNow;

                await _unitOfWork.FuelTypes.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<FuelTypeResponseDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating fuel type");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.FuelTypes.GetByIdAsync(id);

                if (entity == null)
                    return false;

                _unitOfWork.FuelTypes.Remove(entity);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting fuel type with Id: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<FuelTypeResponseDto>> GetAllAsync(bool onlyActive = false)
        {
            try
            {
                IEnumerable<FuelType> entities;

                if (onlyActive)
                {
                    entities = await _unitOfWork.FuelTypes.FindAsync(x => x.IsActive);
                }
                else
                {
                    entities = await _unitOfWork.FuelTypes.GetAllAsync();
                }

                return _mapper.Map<IEnumerable<FuelTypeResponseDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching fuel types");
                throw;
            }
        }

        public async Task<FuelTypeResponseDto?> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.FuelTypes.GetByIdAsync(id);

                if (entity == null)
                    return null;

                return _mapper.Map<FuelTypeResponseDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching fuel type with Id: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ToggleActiveAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.FuelTypes.GetByIdAsync(id);

                if (entity == null)
                    return false;

                entity.IsActive = !entity.IsActive;
                entity.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.FuelTypes.Update(entity);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling fuel type status with Id: {Id}", id);
                throw;
            }
        }

        public async Task<FuelTypeResponseDto?> UpdateAsync(FuelTypeUpdateDto updateDto)
        {
            try
            {
                var entity = await _unitOfWork.FuelTypes.GetByIdAsync(updateDto.Id);

                if (entity == null)
                    return null;

                entity.Name = updateDto.Name;
                entity.Code = updateDto.Code;
                entity.Description = updateDto.Description;
                entity.IconUrl = updateDto.IconUrl;
                entity.SortOrder = updateDto.SortOrder;
                entity.IsActive = updateDto.IsActive;
                entity.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.FuelTypes.Update(entity);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<FuelTypeResponseDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating fuel type with Id: {Id}", updateDto.Id);
                throw;
            }
        }
    }
}