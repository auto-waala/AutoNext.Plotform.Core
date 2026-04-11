using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoNext.Plotform.Core.API.Models.Entities
{
    [Table("document_types")]
    public class DocumentType
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

        [Column("is_required")]
        public bool IsRequired { get; set; } = false;

        [Column("is_verifiable")]
        public bool IsVerifiable { get; set; } = false;

        [Column("expiry_months")]
        public int? ExpiryMonths { get; set; }

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