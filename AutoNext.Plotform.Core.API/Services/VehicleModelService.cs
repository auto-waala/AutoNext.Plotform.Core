using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;

namespace AutoNext.Plotform.Core.API.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public VehicleModelService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VehicleModelResponseDto>> GetAllAsync()
        {
            var data = await _uow.VehicleModels.GetAllAsync();
            return data.Select(x => _mapper.Map<VehicleModelResponseDto>(x));
        }

        public async Task<VehicleModelResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _uow.VehicleModels.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<VehicleModelResponseDto>(entity);
        }

        public async Task<IEnumerable<VehicleModelResponseDto>> GetByBrandAsync(Guid brandId)
        {
            var data = await _uow.VehicleModels
                .FindAsync(x => x.BrandId == brandId && x.IsActive);

            return data.Select(x => _mapper.Map<VehicleModelResponseDto>(x));
        }

        public async Task<VehicleModelResponseDto> CreateAsync(VehicleModelCreateDto dto)
        {
            await _uow.BeginTransactionAsync();

            try
            {
                var entity = _mapper.Map<VehicleModel>(dto);
                entity.Id = Guid.NewGuid();
                entity.CreatedAt = DateTime.UtcNow;

                await _uow.VehicleModels.AddAsync(entity);
                await _uow.SaveChangesAsync();
                await _uow.CommitTransactionAsync();

                return _mapper.Map<VehicleModelResponseDto>(entity);
            }
            catch
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<VehicleModelResponseDto?> UpdateAsync(Guid id, VehicleModelUpdateDto dto)
        {
            await _uow.BeginTransactionAsync();

            try
            {
                var entity = await _uow.VehicleModels.GetByIdAsync(id);
                if (entity == null) return null;

                _mapper.Map(dto, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                _uow.VehicleModels.Update(entity);
                await _uow.SaveChangesAsync();
                await _uow.CommitTransactionAsync();

                return _mapper.Map<VehicleModelResponseDto>(entity);
            }
            catch
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _uow.VehicleModels.GetByIdAsync(id);
            if (entity == null) return false;

            _uow.VehicleModels.Remove(entity);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
