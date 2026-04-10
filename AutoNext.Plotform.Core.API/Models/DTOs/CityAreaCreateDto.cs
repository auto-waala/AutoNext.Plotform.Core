using System.ComponentModel.DataAnnotations;

namespace AutoNext.Plotform.Core.API.Models.DTOs
{
    public class CityAreaCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string AreaName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? AreaCode { get; set; }

        [MaxLength(10)]
        public string? Pincode { get; set; }
    }
}
