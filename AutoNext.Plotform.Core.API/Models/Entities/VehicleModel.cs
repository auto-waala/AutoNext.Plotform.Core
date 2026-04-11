using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoNext.Plotform.Core.API.Models.Entities
{
    [Table("models")] 
    public class VehicleModel
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("brand_id")]
        public Guid BrandId { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column("code")]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [Column("slug")]
        [MaxLength(100)]
        public string Slug { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Required]
        [Column("vehicle_type_id")]
        public Guid VehicleTypeId { get; set; }

        [Column("start_year")]
        public int? StartYear { get; set; }

        [Column("end_year")]
        public int? EndYear { get; set; }

        [Column("is_current_model")]
        public bool IsCurrentModel { get; set; } = false;

        [Column("image_url")]
        [MaxLength(500)]
        public string? ImageUrl { get; set; }

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

        // 🔗 Navigation
        public Brand? Brand { get; set; }
        public VehicleType? VehicleType { get; set; }
    }
}