using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoNext.Plotform.Core.API.Models.Entities
{
    [Table("tax_rates")]
    public class TaxRate
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

        [Column("tax_type")]
        [MaxLength(50)]
        public string? TaxType { get; set; }

        [Column("country")]
        [MaxLength(100)]
        public string? Country { get; set; }

        [Column("state")]
        [MaxLength(100)]
        public string? State { get; set; }

        [Column("city")]
        [MaxLength(100)]
        public string? City { get; set; }

        [Required]
        [Column("rate_percentage")]
        public decimal RatePercentage { get; set; }

        [Column("is_compound")]
        public bool IsCompound { get; set; } = false;

        [Column("applies_to_vehicle_types")]
        public string? AppliesToVehicleTypes { get; set; }

        [Column("min_price_threshold")]
        public decimal? MinPriceThreshold { get; set; }

        [Column("max_price_threshold")]
        public decimal? MaxPriceThreshold { get; set; }

        [Required]
        [Column("effective_from")]
        public DateTime EffectiveFrom { get; set; }

        [Column("effective_to")]
        public DateTime? EffectiveTo { get; set; }

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