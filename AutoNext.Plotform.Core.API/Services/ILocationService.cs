using AutoNext.Plotform.Core.API.Models.DTOs;

namespace AutoNext.Plotform.Core.API.Services
{
    public interface ILocationService
    {
        Task<LocationResponseDto?> GetLocationByIdAsync(Guid locationId);
        Task<IEnumerable<LocationResponseDto>> GetAllLocationsAsync();
        Task<IEnumerable<LocationResponseDto>> GetLocationsByStateAsync(string stateCode);
        Task<IEnumerable<LocationResponseDto>> GetLocationsByCityAsync(string cityName);
        Task<LocationResponseDto> CreateLocationAsync(LocationCreateDto createDto);
        Task<bool> DeleteLocationAsync(Guid locationId);
        Task<IEnumerable<CityAreaDto>> GetAreasByCityAsync(Guid locationId);
    }
}
