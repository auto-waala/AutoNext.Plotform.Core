using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;

namespace AutoNext.Plotform.Core.API.Services
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ServiceTypeService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceTypeResponseDto>> GetAllAsync()
        {
            var data = await _uow.ServiceTypes.GetAllAsync();
            return data.Select(x => _mapper.Map<ServiceTypeResponseDto>(x));
        }

        public async Task<IEnumerable<ServiceTypeResponseDto>> GetActiveAsync()
        {
            var data = await _uow.ServiceTypes.FindAsync(x => x.IsActive);
            return data.OrderBy(x => x.DisplayOrder)
                       .Select(x => _mapper.Map<ServiceTypeResponseDto>(x));
        }

        public async Task<ServiceTypeResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _uow.ServiceTypes.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<ServiceTypeResponseDto>(entity);
        }

        public async Task<ServiceTypeResponseDto> CreateAsync(ServiceTypeCreateDto dto)
        {
            await _uow.BeginTransactionAsync();

            try
            {
                var entity = _mapper.Map<ServiceType>(dto);
                entity.Id = Guid.NewGuid();
                entity.CreatedAt = DateTime.UtcNow;

                await _uow.ServiceTypes.AddAsync(entity);
                await _uow.SaveChangesAsync();
                await _uow.CommitTransactionAsync();

                return _mapper.Map<ServiceTypeResponseDto>(entity);
            }
            catch
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<ServiceTypeResponseDto?> UpdateAsync(Guid id, ServiceTypeUpdateDto dto)
        {
            await _uow.BeginTransactionAsync();

            try
            {
                var entity = await _uow.ServiceTypes.GetByIdAsync(id);
                if (entity == null) return null;

                _mapper.Map(dto, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                _uow.ServiceTypes.Update(entity);
                await _uow.SaveChangesAsync();
                await _uow.CommitTransactionAsync();

                return _mapper.Map<ServiceTypeResponseDto>(entity);
            }
            catch
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _uow.ServiceTypes.GetByIdAsync(id);
            if (entity == null) return false;

            _uow.ServiceTypes.Remove(entity);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleStatusAsync(Guid id, bool isActive)
        {
            var entity = await _uow.ServiceTypes.GetByIdAsync(id);
            if (entity == null) return false;

            entity.IsActive = isActive;
            entity.UpdatedAt = DateTime.UtcNow;

            _uow.ServiceTypes.Update(entity);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}