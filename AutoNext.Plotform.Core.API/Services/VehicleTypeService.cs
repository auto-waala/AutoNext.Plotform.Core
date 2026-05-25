using AutoMapper;
using AutoNext.Plotform.Core.API.Data.Context;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoNext.Plotform.Core.API.Services
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<VehicleTypeService> _logger;
        private readonly ApplicationDbContext _db;

        public VehicleTypeService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<VehicleTypeService> logger,
            ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _db = db;
        }

        public async Task<VehicleTypeResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _db.VehicleTypes
                .AsNoTracking()
                .IgnoreAutoIncludes()
                .FirstOrDefaultAsync(v => v.Id == id);

            return entity == null ? null : _mapper.Map<VehicleTypeResponseDto>(entity);
        }

        public async Task<IEnumerable<VehicleTypeResponseDto>> GetAllAsync(bool onlyActive = false)
        {
            var query = _db.VehicleTypes.AsQueryable();

            if (onlyActive)
                query = query.Where(v => v.IsActive);

            var entities = await query.OrderBy(v => v.DisplayOrder).ThenBy(v => v.Name).ToListAsync();

            _logger.LogInformation("VehicleTypes fetched: {Count}", entities.Count);

            return _mapper.Map<IEnumerable<VehicleTypeResponseDto>>(entities);
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

                _logger.LogInformation("Created VehicleType: {Name} ({Code})", entity.Name, entity.Code);
                return _mapper.Map<VehicleTypeResponseDto>(entity);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error creating VehicleType");
                throw;
            }
        }

        public async Task<VehicleTypeResponseDto?> UpdateAsync(VehicleTypeUpdateDto updateDto)
        {
            var entity = await _db.VehicleTypes
                .IgnoreAutoIncludes()
                .FirstOrDefaultAsync(v => v.Id == updateDto.Id);

            if (entity == null) return null;

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _mapper.Map(updateDto, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.VehicleTypes.Update(entity);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Updated VehicleType: {Name} ({Code})", entity.Name, entity.Code);
                return _mapper.Map<VehicleTypeResponseDto>(entity);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error updating VehicleType ID: {Id}", updateDto.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _db.VehicleTypes
                .IgnoreAutoIncludes()
                .FirstOrDefaultAsync(v => v.Id == id);

            if (entity == null) return false;

            _unitOfWork.VehicleTypes.Remove(entity);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Deleted VehicleType ID: {Id}", id);
            return true;
        }

        public async Task<bool> ToggleActiveAsync(Guid id)
        {
            var entity = await _db.VehicleTypes
                .IgnoreAutoIncludes()
                .FirstOrDefaultAsync(v => v.Id == id);

            if (entity == null) return false;

            entity.IsActive = !entity.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.VehicleTypes.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Toggled VehicleType ID: {Id} → IsActive={Status}", id, entity.IsActive);
            return true;
        }
    }
}