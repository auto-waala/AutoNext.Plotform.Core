using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoNext.Plotform.Core.API.Models.Entities
{
    [Table("inspection_checklist")]
    public class InspectionChecklist
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

        [Column("applicable_vehicle_types")]
        public string? ApplicableVehicleTypes { get; set; } // JSONB

        [Column("is_critical")]
        public bool IsCritical { get; set; } = false;

        [Column("weightage")]
        public int Weightage { get; set; } = 1;

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