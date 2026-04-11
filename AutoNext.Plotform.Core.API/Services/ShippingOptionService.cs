using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;

namespace AutoNext.Plotform.Core.API.Services
{
    public class ShippingOptionService : IShippingOptionService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ShippingOptionService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShippingOptionResponseDto>> GetAllAsync()
        {
            var data = await _uow.ShippingOptions.GetAllAsync();
            return data.Select(x => _mapper.Map<ShippingOptionResponseDto>(x));
        }

        public async Task<IEnumerable<ShippingOptionResponseDto>> GetActiveAsync()
        {
            var data = await _uow.ShippingOptions.FindAsync(x => x.IsActive);
            return data.OrderBy(x => x.DisplayOrder)
                       .Select(x => _mapper.Map<ShippingOptionResponseDto>(x));
        }

        public async Task<ShippingOptionResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _uow.ShippingOptions.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<ShippingOptionResponseDto>(entity);
        }

        public async Task<ShippingOptionResponseDto> CreateAsync(ShippingOptionCreateDto dto)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var entity = _mapper.Map<ShippingOption>(dto);
                entity.Id = Guid.NewGuid();
                entity.CreatedAt = DateTime.UtcNow;

                await _uow.ShippingOptions.AddAsync(entity);
                await _uow.SaveChangesAsync();
                await _uow.CommitTransactionAsync();

                return _mapper.Map<ShippingOptionResponseDto>(entity);
            }
            catch
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<ShippingOptionResponseDto?> UpdateAsync(Guid id, ShippingOptionUpdateDto dto)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var entity = await _uow.ShippingOptions.GetByIdAsync(id);
                if (entity == null) return null;

                _mapper.Map(dto, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                _uow.ShippingOptions.Update(entity);
                await _uow.SaveChangesAsync();
                await _uow.CommitTransactionAsync();

                return _mapper.Map<ShippingOptionResponseDto>(entity);
            }
            catch
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _uow.ShippingOptions.GetByIdAsync(id);
            if (entity == null) return false;

            _uow.ShippingOptions.Remove(entity);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleStatusAsync(Guid id, bool isActive)
        {
            var entity = await _uow.ShippingOptions.GetByIdAsync(id);
            if (entity == null) return false;

            entity.IsActive = isActive;
            entity.UpdatedAt = DateTime.UtcNow;

            _uow.ShippingOptions.Update(entity);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}