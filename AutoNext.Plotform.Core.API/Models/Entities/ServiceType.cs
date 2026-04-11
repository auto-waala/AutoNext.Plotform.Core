using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoNext.Plotform.Core.API.Models.Entities
{
    [Table("service_types")]
    public class ServiceType
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column("code")]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        [Column("category")]
        [MaxLength(50)]
        public string? Category { get; set; }

        [Column("interval_months")]
        public int? IntervalMonths { get; set; }

        [Column("interval_km")]
        public int? IntervalKm { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("icon_url")]
        [MaxLength(500)]
        public string? IconUrl { get; set; }

        [Column("display_order")]
        public int DisplayOrder { get; set; } = 0;

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}