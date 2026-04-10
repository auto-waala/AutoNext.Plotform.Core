using AutoMapper;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Models.DTOs;
using AutoNext.Plotform.Core.API.Models.Entities;

namespace AutoNext.Plotform.Core.API.Services
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<LocationService> _logger;

        public LocationService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<LocationService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<LocationResponseDto?> GetLocationByIdAsync(Guid locationId)
        {
            try
            {
                var location = await _unitOfWork.Locations.GetByIdAsync(locationId);
                if (location == null)
                    return null;

                var locationDto = _mapper.Map<LocationResponseDto>(location);

                // Load areas
                var areas = await _unitOfWork.CityAreas
                    .FindAsync(a => a.LocationId == locationId);
                locationDto.Areas = _mapper.Map<List<CityAreaDto>>(areas);

                return locationDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting location by ID: {Id}", locationId);
                throw;
            }
        }

        public async Task<IEnumerable<LocationResponseDto>> GetAllLocationsAsync()
        {
            var locations = await _unitOfWork.Locations.GetAllAsync();
            return _mapper.Map<IEnumerable<LocationResponseDto>>(locations);
        }

        public async Task<IEnumerable<LocationResponseDto>> GetLocationsByStateAsync(string stateCode)
        {
            var locations = await _unitOfWork.Locations
                .FindAsync(l => l.StateCode == stateCode);
            return _mapper.Map<IEnumerable<LocationResponseDto>>(locations);
        }

        public async Task<IEnumerable<LocationResponseDto>> GetLocationsByCityAsync(string cityName)
        {
            var locations = await _unitOfWork.Locations
                .FindAsync(l => l.CityName.Contains(cityName));
            return _mapper.Map<IEnumerable<LocationResponseDto>>(locations);
        }

        public async Task<LocationResponseDto> CreateLocationAsync(LocationCreateDto createDto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var location = _mapper.Map<Location>(createDto);
                await _unitOfWork.Locations.AddAsync(location);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Created new location: {City}, {State}",
                    location.CityName, location.StateName);

                return _mapper.Map<LocationResponseDto>(location);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error creating location");
                throw;
            }
        }

        public async Task<bool> DeleteLocationAsync(Guid locationId)
        {
            var location = await _unitOfWork.Locations.GetByIdAsync(locationId);
            if (location == null)
                return false;

            _unitOfWork.Locations.Remove(location);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CityAreaDto>> GetAreasByCityAsync(Guid locationId)
        {
            var areas = await _unitOfWork.CityAreas
                .FindAsync(a => a.LocationId == locationId);
            return _mapper.Map<IEnumerable<CityAreaDto>>(areas);
        }
    }
}
