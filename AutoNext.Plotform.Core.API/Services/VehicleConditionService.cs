using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;

namespace AutoNext.Plotform.Core.API.Services
{
    public class VehicleConditionService : IVehicleConditionService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public VehicleConditionService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VehicleConditionResponseDto>> GetAllAsync()
        {
            var data = await _uow.VehicleConditions.GetAllAsync();
            return data.Select(x => _mapper.Map<VehicleConditionResponseDto>(x));
        }

        public async Task<IEnumerable<VehicleConditionResponseDto>> GetActiveAsync()
        {
            var data = await _uow.VehicleConditions.FindAsync(x => x.IsActive);
            return data.OrderBy(x => x.DisplayOrder)
                       .Select(x => _mapper.Map<VehicleConditionResponseDto>(x));
        }

        public async Task<VehicleConditionResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _uow.VehicleConditions.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<VehicleConditionResponseDto>(entity);
        }

        public async Task<VehicleConditionResponseDto> CreateAsync(VehicleConditionCreateDto dto)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var entity = _mapper.Map<VehicleCondition>(dto);
                entity.Id = Guid.NewGuid();
                entity.CreatedAt = DateTime.UtcNow;

                await _uow.VehicleConditions.AddAsync(entity);
                await _uow.SaveChangesAsync();
                await _uow.CommitTransactionAsync();

                return _mapper.Map<VehicleConditionResponseDto>(entity);
            }
            catch
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<VehicleConditionResponseDto?> UpdateAsync(Guid id, VehicleConditionUpdateDto dto)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var entity = await _uow.VehicleConditions.GetByIdAsync(id);
                if (entity == null) return null;

                _mapper.Map(dto, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                _uow.VehicleConditions.Update(entity);
                await _uow.SaveChangesAsync();
                await _uow.CommitTransactionAsync();

                return _mapper.Map<VehicleConditionResponseDto>(entity);
            }
            catch
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _uow.VehicleConditions.GetByIdAsync(id);
            if (entity == null) return false;

            _uow.VehicleConditions.Remove(entity);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleStatusAsync(Guid id, bool isActive)
        {
            var entity = await _uow.VehicleConditions.GetByIdAsync(id);
            if (entity == null) return false;

            entity.IsActive = isActive;
            entity.UpdatedAt = DateTime.UtcNow;

            _uow.VehicleConditions.Update(entity);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}