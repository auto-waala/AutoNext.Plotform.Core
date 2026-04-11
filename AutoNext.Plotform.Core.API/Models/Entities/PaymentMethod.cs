using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoNext.Plotform.Core.API.Models.Entities
{
    [Table("payment_methods")]
    public class PaymentMethod
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

        [Column("type")]
        [MaxLength(50)]
        public string? Type { get; set; }

        [Column("icon_url")]
        [MaxLength(500)]
        public string? IconUrl { get; set; }

        [Column("processing_fee_percentage")]
        public decimal ProcessingFeePercentage { get; set; } = 0;

        [Column("processing_fee_fixed")]
        public decimal ProcessingFeeFixed { get; set; } = 0;

        [Column("settlement_days")]
        public int? SettlementDays { get; set; }

        [Column("is_instant")]
        public bool IsInstant { get; set; } = false;

        [Column("is_available_for_sellers")]
        public bool IsAvailableForSellers { get; set; } = true;

        [Column("is_available_for_buyers")]
        public bool IsAvailableForBuyers { get; set; } = true;

        [Column("display_order")]
        public int DisplayOrder { get; set; } = 0;

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("metadata")]
        public string? Metadata { get; set; } // JSONB

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}