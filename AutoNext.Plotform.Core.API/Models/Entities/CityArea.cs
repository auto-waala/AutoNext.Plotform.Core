using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoNext.Plotform.Core.API.Models.Entities
{
    [Table("city_areas")]
    public class CityArea
    {
        [Key]
        [Column("id")] 
        public Guid Id { get; set; }

        [Required]
        [Column("location_id")]
        public Guid LocationId { get; set; }

        [Required]
        [Column("area_name")]
        [MaxLength(200)]
        public string AreaName { get; set; } = string.Empty;

        [Column("area_code")]
        [MaxLength(20)]
        public string? AreaCode { get; set; }

        [Column("pincode")]
        [MaxLength(10)]
        public string? Pincode { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(LocationId))]
        public virtual Location? Location { get; set; }
    }
}