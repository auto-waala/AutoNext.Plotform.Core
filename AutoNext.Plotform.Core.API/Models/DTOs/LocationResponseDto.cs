namespace AutoNext.Plotform.Core.API.Models.DTOs
{
    public class LocationResponseDto
    {
        public Guid Id { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;
        public string StateCode { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string? District { get; set; }
        public string? Pincode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public List<CityAreaDto>? Areas { get; set; }
    }

}
