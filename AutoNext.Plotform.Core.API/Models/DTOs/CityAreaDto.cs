namespace AutoNext.Plotform.Core.API.Models.DTOs
{
    public class CityAreaDto
    {
        public Guid Id { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public string? AreaCode { get; set; }
        public string? Pincode { get; set; }
    }
}
