using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoNext.Plotform.Core.API.Models.Entities
{
    [Table("locations")]
    public class Location
    {
        [Key]
        [Column("id")] 
        public Guid Id { get; set; }

        [Required]
        [Column("country_name")]
        [MaxLength(100)]
        public string CountryName { get; set; } = string.Empty;

        [Required]
        [Column("country_code")]
        [MaxLength(5)]
        public string CountryCode { get; set; } = string.Empty;

        [Required]
        [Column("state_name")]
        [MaxLength(100)]
        public string StateName { get; set; } = string.Empty;

        [Required]
        [Column("state_code")]
        [MaxLength(10)]
        public string StateCode { get; set; } = string.Empty;

        [Required]
        [Column("city_name")]
        [MaxLength(100)]
        public string CityName { get; set; } = string.Empty;

        [Column("district")]
        [MaxLength(100)]
        public string? District { get; set; }

        [Column("pincode")]
        [MaxLength(10)]
        public string? Pincode { get; set; }

        [Column("latitude")]
        public decimal? Latitude { get; set; }

        [Column("longitude")]
        public decimal? Longitude { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation property
        public virtual ICollection<CityArea> Areas { get; set; } = new List<CityArea>();
    }
}