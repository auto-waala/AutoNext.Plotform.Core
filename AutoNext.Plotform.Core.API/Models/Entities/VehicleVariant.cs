using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoNext.Plotform.Core.API.Models.Entities
{
    [Table("vehicle_variants")]
    public class VehicleVariant
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("model_id")]
        public Guid ModelId { get; set; }

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

        [Column("fuel_type_id")]
        public Guid? FuelTypeId { get; set; }

        [Column("transmission_id")]
        public Guid? TransmissionId { get; set; }

        [Column("drive_type")]
        [MaxLength(50)]
        public string? DriveType { get; set; }

        [Column("engine_size")]
        public decimal? EngineSize { get; set; }

        [Column("horsepower")]
        public int? Horsepower { get; set; }

        [Column("torque")]
        public int? Torque { get; set; }

        [Column("seating_capacity")]
        public int? SeatingCapacity { get; set; }

        [Column("doors_count")]
        public int? DoorsCount { get; set; }

        [Column("base_price")]
        public decimal? BasePrice { get; set; }

        [Column("is_available")]
        public bool IsAvailable { get; set; } = true;

        [Column("display_order")]
        public int DisplayOrder { get; set; } = 0;

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("metadata")]
        public string? Metadata { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey(nameof(ModelId))]
        public virtual VehicleModel? Model { get; set; }

        [ForeignKey(nameof(FuelTypeId))]
        public virtual FuelType? FuelType { get; set; }

        [ForeignKey(nameof(TransmissionId))]
        public virtual Transmission? Transmission { get; set; }
    }
}