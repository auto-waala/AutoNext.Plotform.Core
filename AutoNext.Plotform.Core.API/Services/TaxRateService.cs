using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;

namespace AutoNext.Plotform.Core.API.Services
{
    public class TaxRateService : ITaxRateService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TaxRateService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaxRateResponseDto>> GetAllAsync()
        {
            var data = await _uow.TaxRates.GetAllAsync();
            return data.Select(x => _mapper.Map<TaxRateResponseDto>(x));
        }

        public async Task<IEnumerable<TaxRateResponseDto>> GetActiveAsync()
        {
            var data = await _uow.TaxRates.FindAsync(x => x.IsActive);
            return data.Select(x => _mapper.Map<TaxRateResponseDto>(x));
        }

        public async Task<TaxRateResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _uow.TaxRates.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<TaxRateResponseDto>(entity);
        }

        public async Task<TaxRateResponseDto> CreateAsync(TaxRateCreateDto dto)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var entity = _mapper.Map<TaxRate>(dto);
                entity.Id = Guid.NewGuid();
                entity.CreatedAt = DateTime.UtcNow;

                await _uow.TaxRates.AddAsync(entity);
                await _uow.SaveChangesAsync();
                await _uow.CommitTransactionAsync();

                return _mapper.Map<TaxRateResponseDto>(entity);
            }
            catch
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<TaxRateResponseDto?> UpdateAsync(Guid id, TaxRateUpdateDto dto)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var entity = await _uow.TaxRates.GetByIdAsync(id);
                if (entity == null) return null;

                _mapper.Map(dto, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                _uow.TaxRates.Update(entity);
                await _uow.SaveChangesAsync();
                await _uow.CommitTransactionAsync();

                return _mapper.Map<TaxRateResponseDto>(entity);
            }
            catch
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _uow.TaxRates.GetByIdAsync(id);
            if (entity == null) return false;

            _uow.TaxRates.Remove(entity);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleStatusAsync(Guid id, bool isActive)
        {
            var entity = await _uow.TaxRates.GetByIdAsync(id);
            if (entity == null) return false;

            entity.IsActive = isActive;
            entity.UpdatedAt = DateTime.UtcNow;

            _uow.TaxRates.Update(entity);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}