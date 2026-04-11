using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoNext.Plotform.Core.API.Models.Entities
{
    [Table("shipping_options")]
    public class ShippingOption
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

        [Column("description")]
        public string? Description { get; set; }

        [Column("provider")]
        [MaxLength(100)]
        public string? Provider { get; set; }

        [Column("estimated_days_min")]
        public int? EstimatedDaysMin { get; set; }

        [Column("estimated_days_max")]
        public int? EstimatedDaysMax { get; set; }

        [Column("base_cost")]
        public decimal? BaseCost { get; set; }

        [Column("cost_per_km")]
        public decimal? CostPerKm { get; set; }

        [Column("is_tracking_available")]
        public bool IsTrackingAvailable { get; set; } = false;

        [Column("is_insurance_available")]
        public bool IsInsuranceAvailable { get; set; } = false;

        [Column("applicable_vehicle_types")]
        public string? ApplicableVehicleTypes { get; set; }

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