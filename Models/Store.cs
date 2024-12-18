namespace GEEKecommerce_API.Models 
{
    public class Store
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }

        // Navigation property
        public ICollection<Inventory>? Inventories { get; set; }
    }

}