using System.ComponentModel.DataAnnotations;

namespace AutoNext.Plotform.Core.API.Models.DTOs
{
    public class CategoryCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Slug { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(500)]
        public string? IconUrl { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public int DisplayOrder { get; set; } = 0;

        public Dictionary<string, object>? Metadata { get; set; }
    }

    public class CategoryUpdateDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? Code { get; set; }

        [MaxLength(100)]
        public string? Slug { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(500)]
        public string? IconUrl { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public int? DisplayOrder { get; set; }

        public bool? IsActive { get; set; }

        public Dictionary<string, object>? Metadata { get; set; }
    }

    public class CategoryResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public Dictionary<string, object>? Metadata { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<CategoryResponseDto> SubCategories { get; set; } = new List<CategoryResponseDto>();
    }

    public class CategoryTreeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        public int DisplayOrder { get; set; }
        public List<CategoryTreeDto> Children { get; set; } = new List<CategoryTreeDto>();
    }
}