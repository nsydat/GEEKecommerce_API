namespace GEEKecommerce_API.Models 
{
    public class Address
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Commune { get; set; }
        public string? Details { get; set; }

        // Navigation property
        public User? User { get; set; }
    }

}