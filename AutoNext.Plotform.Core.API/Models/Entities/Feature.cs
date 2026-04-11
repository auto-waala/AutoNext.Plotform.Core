using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoNext.Plotform.Core.API.Models.Entities
{
    [Table("features")]
    public class Feature
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

        [Column("sub_category")]
        [MaxLength(50)]
        public string? SubCategory { get; set; }

        [Column("icon_url")]
        [MaxLength(500)]
        public string? IconUrl { get; set; }

        [Column("applicable_categories")]
        public string? ApplicableCategories { get; set; } // JSONB

        [Column("is_standard")]
        public bool IsStandard { get; set; } = false;

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
    }
}