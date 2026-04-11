using System.ComponentModel.DataAnnotations;

namespace AutoNext.Plotform.Core.API.Models.DTOs
{
    public class VehicleConditionCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int DisplayOrder { get; set; } = 0;
    }

    public class VehicleConditionUpdateDto
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }

        public int? DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
    }

    public class VehicleConditionResponseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}