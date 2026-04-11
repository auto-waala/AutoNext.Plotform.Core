using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace AutoNext.Plotform.Core.API.Services
{
    public class ColorService : IColorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ColorService> _logger;

        public ColorService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<ColorService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ColorResponseDto?> GetColorByIdAsync(Guid colorId)
        {
            try
            {
                var color = await _unitOfWork.Colors.GetByIdAsync(colorId);
                if (color == null)
                    return null;

                return _mapper.Map<ColorResponseDto>(color);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting color by ID: {Id}", colorId);
                throw;
            }
        }

        public async Task<ColorResponseDto?> GetColorByCodeAsync(string code)
        {
            try
            {
                var colors = await _unitOfWork.Colors
                    .FindAsync(c => c.Code == code);
                var color = colors.FirstOrDefault();

                if (color == null)
                    return null;

                return _mapper.Map<ColorResponseDto>(color);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting color by code: {Code}", code);
                throw;
            }
        }

        public async Task<IEnumerable<ColorResponseDto>> GetAllColorsAsync()
        {
            var colors = await _unitOfWork.Colors.GetAllAsync();
            return _mapper.Map<IEnumerable<ColorResponseDto>>(colors)
                .OrderBy(c => c.DisplayOrder);
        }

        public async Task<IEnumerable<ColorResponseDto>> GetActiveColorsAsync()
        {
            var colors = await _unitOfWork.Colors
                .FindAsync(c => c.IsActive == true);
            return _mapper.Map<IEnumerable<ColorResponseDto>>(colors)
                .OrderBy(c => c.DisplayOrder);
        }

        public async Task<IEnumerable<ColorResponseDto>> GetColorsByDisplayOrderAsync()
        {
            var colors = await _unitOfWork.Colors.GetAllAsync();
            return _mapper.Map<IEnumerable<ColorResponseDto>>(colors)
                .OrderBy(c => c.DisplayOrder)
                .ThenBy(c => c.Name);
        }

        public async Task<ColorResponseDto> CreateColorAsync(ColorCreateDto createDto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Check if code already exists
                var existingColors = await _unitOfWork.Colors
                    .FindAsync(c => c.Code == createDto.Code);
                if (existingColors.Any())
                {
                    throw new InvalidOperationException($"Color with code '{createDto.Code}' already exists");
                }

                // Auto-convert hex to RGB if only hex is provided
                if (!string.IsNullOrEmpty(createDto.HexCode) && string.IsNullOrEmpty(createDto.RgbValue))
                {
                    createDto.RgbValue = await ConvertHexToRgbAsync(createDto.HexCode);
                }

                var color = _mapper.Map<Color>(createDto);
                color.Id = Guid.NewGuid();
                color.CreatedAt = DateTime.UtcNow;

                await _unitOfWork.Colors.AddAsync(color);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Created new color: {Name} with code: {Code}",
                    color.Name, color.Code);

                return _mapper.Map<ColorResponseDto>(color);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error creating color");
                throw;
            }
        }

        public async Task<ColorResponseDto?> UpdateColorAsync(Guid colorId, ColorUpdateDto updateDto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var color = await _unitOfWork.Colors.GetByIdAsync(colorId);
                if (color == null)
                    return null;

                // Check if code already exists (if code is being changed)
                if (!string.IsNullOrEmpty(updateDto.Code) && updateDto.Code != color.Code)
                {
                    var existingColors = await _unitOfWork.Colors
                        .FindAsync(c => c.Code == updateDto.Code);
                    if (existingColors.Any())
                    {
                        throw new InvalidOperationException($"Color with code '{updateDto.Code}' already exists");
                    }
                }

                // Auto-convert hex to RGB if hex is updated
                if (updateDto.HexCode != null && updateDto.HexCode != color.HexCode)
                {
                    updateDto.RgbValue = await ConvertHexToRgbAsync(updateDto.HexCode);
                }

                _mapper.Map(updateDto, color);
                color.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.Colors.Update(color);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Updated color: {Name} with ID: {Id}", color.Name, color.Id);

                return _mapper.Map<ColorResponseDto>(color);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error updating color with ID: {Id}", colorId);
                throw;
            }
        }

        public async Task<bool> DeleteColorAsync(Guid colorId)
        {
            var color = await _unitOfWork.Colors.GetByIdAsync(colorId);
            if (color == null)
                return false;

            _unitOfWork.Colors.Remove(color);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Deleted color with ID: {Id}", colorId);
            return true;
        }

        public async Task<bool> ToggleColorStatusAsync(Guid colorId, bool isActive)
        {
            var color = await _unitOfWork.Colors.GetByIdAsync(colorId);
            if (color == null)
                return false;

            color.IsActive = isActive;
            color.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Colors.Update(color);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Color {Id} status changed to: {Status}", colorId, isActive);
            return true;
        }

        public async Task<bool> ReorderColorsAsync(Dictionary<Guid, int> orderMap)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                foreach (var item in orderMap)
                {
                    var color = await _unitOfWork.Colors.GetByIdAsync(item.Key);
                    if (color != null)
                    {
                        color.DisplayOrder = item.Value;
                        color.UpdatedAt = DateTime.UtcNow;
                        _unitOfWork.Colors.Update(color);
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Reordered {Count} colors", orderMap.Count);
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error reordering colors");
                throw;
            }
        }

        public async Task<bool> ValidateHexCodeAsync(string hexCode)
        {
            if (string.IsNullOrEmpty(hexCode))
                return false;

            var regex = new Regex(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$");
            return await Task.FromResult(regex.IsMatch(hexCode));
        }

        public async Task<string?> ConvertHexToRgbAsync(string hexCode)
        {
            if (string.IsNullOrEmpty(hexCode))
                return null;

            try
            {
                // Remove # if present
                hexCode = hexCode.TrimStart('#');

                // Handle 3-digit hex
                if (hexCode.Length == 3)
                {
                    hexCode = new string(new char[]
                    {
                        hexCode[0], hexCode[0],
                        hexCode[1], hexCode[1],
                        hexCode[2], hexCode[2]
                    });
                }

                // Convert to RGB
                int r = int.Parse(hexCode.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                int g = int.Parse(hexCode.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                int b = int.Parse(hexCode.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

                return await Task.FromResult($"rgb({r}, {g}, {b})");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error converting hex to RGB: {HexCode}", hexCode);
                return null;
            }
        }
    }
}