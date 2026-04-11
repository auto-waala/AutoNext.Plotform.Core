using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;

namespace AutoNext.Plotform.Core.API.Services
{
    public class TitleTypeService : ITitleTypeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TitleTypeService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TitleTypeResponseDto>> GetAllAsync()
        {
            var data = await _uow.TitleTypes.GetAllAsync();
            return data.Select(x => _mapper.Map<TitleTypeResponseDto>(x));
        }

        public async Task<IEnumerable<TitleTypeResponseDto>> GetActiveAsync()
        {
            var data = await _uow.TitleTypes.FindAsync(x => x.IsActive);
            return data.OrderBy(x => x.DisplayOrder)
                       .Select(x => _mapper.Map<TitleTypeResponseDto>(x));
        }

        public async Task<TitleTypeResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _uow.TitleTypes.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<TitleTypeResponseDto>(entity);
        }

        public async Task<TitleTypeResponseDto> CreateAsync(TitleTypeCreateDto dto)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var entity = _mapper.Map<TitleType>(dto);
                entity.Id = Guid.NewGuid();
                entity.CreatedAt = DateTime.UtcNow;

                await _uow.TitleTypes.AddAsync(entity);
                await _uow.SaveChangesAsync();
                await _uow.CommitTransactionAsync();

                return _mapper.Map<TitleTypeResponseDto>(entity);
            }
            catch
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<TitleTypeResponseDto?> UpdateAsync(Guid id, TitleTypeUpdateDto dto)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var entity = await _uow.TitleTypes.GetByIdAsync(id);
                if (entity == null) return null;

                _mapper.Map(dto, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                _uow.TitleTypes.Update(entity);
                await _uow.SaveChangesAsync();
                await _uow.CommitTransactionAsync();

                return _mapper.Map<TitleTypeResponseDto>(entity);
            }
            catch
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _uow.TitleTypes.GetByIdAsync(id);
            if (entity == null) return false;

            _uow.TitleTypes.Remove(entity);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleStatusAsync(Guid id, bool isActive)
        {
            var entity = await _uow.TitleTypes.GetByIdAsync(id);
            if (entity == null) return false;

            entity.IsActive = isActive;
            entity.UpdatedAt = DateTime.UtcNow;

            _uow.TitleTypes.Update(entity);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}