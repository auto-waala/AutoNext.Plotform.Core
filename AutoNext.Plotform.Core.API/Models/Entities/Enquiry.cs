using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoNext.Plotform.Core.API.Models.Entities
{
    [Table("enquiries")]
    public class Enquiry
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column("email")]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("phone")]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [Column("category")]
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        [Column("city")]
        [MaxLength(100)]
        public string? City { get; set; }

        [Column("message")]
        public string? Message { get; set; }

        [Column("status")]
        [MaxLength(30)]
        public string Status { get; set; } = "Pending";

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}