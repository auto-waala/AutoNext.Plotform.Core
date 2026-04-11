using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;
using Microsoft.Extensions.Logging;

namespace AutoNext.Plotform.Core.API.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentMethodService> _logger;

        public PaymentMethodService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PaymentMethodService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<PaymentMethodResponseDto>> GetAllAsync()
        {
            var data = await _unitOfWork.PaymentMethods.GetAllAsync();
            return data.Select(x => _mapper.Map<PaymentMethodResponseDto>(x));
        }

        public async Task<IEnumerable<PaymentMethodResponseDto>> GetActiveAsync()
        {
            var data = await _unitOfWork.PaymentMethods
                .FindAsync(x => x.IsActive);

            return data.OrderBy(x => x.DisplayOrder)
                       .Select(x => _mapper.Map<PaymentMethodResponseDto>(x));
        }

        public async Task<PaymentMethodResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.PaymentMethods.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<PaymentMethodResponseDto>(entity);
        }

        public async Task<IEnumerable<PaymentMethodResponseDto>> GetByTypeAsync(string type)
        {
            var data = await _unitOfWork.PaymentMethods
                .FindAsync(x => x.Type == type && x.IsActive);

            return data.Select(x => _mapper.Map<PaymentMethodResponseDto>(x));
        }

        public async Task<PaymentMethodResponseDto> CreateAsync(PaymentMethodCreateDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var entity = _mapper.Map<PaymentMethod>(dto);
                entity.Id = Guid.NewGuid();
                entity.CreatedAt = DateTime.UtcNow;

                await _unitOfWork.PaymentMethods.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Created PaymentMethod: {Code}", entity.Code);

                return _mapper.Map<PaymentMethodResponseDto>(entity);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error creating PaymentMethod");
                throw;
            }
        }

        public async Task<PaymentMethodResponseDto?> UpdateAsync(Guid id, PaymentMethodUpdateDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var entity = await _unitOfWork.PaymentMethods.GetByIdAsync(id);
                if (entity == null) return null;

                _mapper.Map(dto, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.PaymentMethods.Update(entity);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Updated PaymentMethod: {Id}", id);

                return _mapper.Map<PaymentMethodResponseDto>(entity);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error updating PaymentMethod: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _unitOfWork.PaymentMethods.GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.PaymentMethods.Remove(entity);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Deleted PaymentMethod: {Id}", id);
            return true;
        }

        public async Task<bool> ToggleStatusAsync(Guid id, bool isActive)
        {
            var entity = await _unitOfWork.PaymentMethods.GetByIdAsync(id);
            if (entity == null) return false;

            entity.IsActive = isActive;
            entity.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.PaymentMethods.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("PaymentMethod {Id} status changed to {Status}", id, isActive);
            return true;
        }
    }
}