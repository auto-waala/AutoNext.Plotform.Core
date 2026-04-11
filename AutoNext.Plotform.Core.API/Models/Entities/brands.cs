using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoNext.Plotform.Core.API.Models.Entities
{
    [Table("brands")]
    public class Brand
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

        [Required]
        [Column("slug")]
        [MaxLength(100)]
        public string Slug { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("logo_url")]
        [MaxLength(500)]
        public string? LogoUrl { get; set; }

        [Column("website_url")]
        [MaxLength(255)]
        public string? WebsiteUrl { get; set; }

        [Column("country_of_origin")]
        [MaxLength(100)]
        public string? CountryOfOrigin { get; set; }

        [Column("founded_year")]
        public int? FoundedYear { get; set; }

        [Column("applicable_categories")]
        public string? ApplicableCategories { get; set; }

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