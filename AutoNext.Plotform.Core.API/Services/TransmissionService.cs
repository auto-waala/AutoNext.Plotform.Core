using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;

namespace AutoNext.Plotform.Core.API.Services
{
    public class TransmissionService : ITransmissionService
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

        public async Task<TransmissionResponseDto> CreateAsync(TransmissionCreateDto createDto)
        {
            try
            {
                var entity = _mapper.Map<Transmission>(createDto);

                entity.Id = Guid.NewGuid();
                entity.IsActive = true;
                entity.CreatedAt = DateTime.UtcNow;

                await _unitOfWork.Transmissions.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<TransmissionResponseDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating transmission");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Transmissions.GetByIdAsync(id);

                if (entity == null)
                    return false;

                _unitOfWork.Transmissions.Remove(entity);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting transmission with Id: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<TransmissionResponseDto>> GetAllAsync(bool onlyActive = false)
        {
            try
            {
                IEnumerable<Transmission> entities;

                if (onlyActive)
                {
                    entities = await _unitOfWork.Transmissions.FindAsync(x => x.IsActive);
                }
                else
                {
                    entities = await _unitOfWork.Transmissions.GetAllAsync();
                }

                return _mapper.Map<IEnumerable<TransmissionResponseDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching transmissions");
                throw;
            }
        }

        public async Task<IEnumerable<TransmissionResponseDto>> GetByGearsCountAsync(int gearsCount)
        {
            var entities = await _unitOfWork.Transmissions
                .FindAsync(t => t.GearsCount == gearsCount && t.IsActive);

            return _mapper.Map<IEnumerable<TransmissionResponseDto>>(entities);
        }

        public async Task<TransmissionResponseDto?> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Transmissions.GetByIdAsync(id);

                if (entity == null)
                    return null;

                return _mapper.Map<TransmissionResponseDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching transmission with Id: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ToggleActiveAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Transmissions.GetByIdAsync(id);

                if (entity == null)
                    return false;

                entity.IsActive = !entity.IsActive;
                entity.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.Transmissions.Update(entity);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling transmission status with Id: {Id}", id);
                throw;
            }
        }

        public async Task<TransmissionResponseDto?> UpdateAsync(TransmissionUpdateDto updateDto)
        {
            try
            {
                var entity = await _unitOfWork.Transmissions.GetByIdAsync(updateDto.Id);

                if (entity == null)
                    return null;

                entity.Name = updateDto.Name;
                entity.Code = updateDto.Code;
                entity.Description = updateDto.Description;
                entity.GearsCount = updateDto.GearsCount;
                entity.SortOrder = updateDto.SortOrder;
                entity.IsActive = updateDto.IsActive;
                entity.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.Transmissions.Update(entity);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<TransmissionResponseDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating transmission with Id: {Id}", updateDto.Id);
                throw;
            }
        }
    }
}