using System.ComponentModel.DataAnnotations;

namespace AutoNext.Plotform.Core.API.Models.DTOs
{
    public class LocationCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string CountryName { get; set; } = string.Empty;

        [Required]
        [MaxLength(5)]
        public string CountryCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string StateName { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string StateCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string CityName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? District { get; set; }

        [MaxLength(10)]
        public string? Pincode { get; set; }

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
