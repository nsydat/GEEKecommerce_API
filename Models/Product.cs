namespace GEEKecommerce_API.Models 
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public int? CategoryId { get; set; }
        public int? Stock { get; set; }

        // Navigation properties
        public Category? Category { get; set; }
        public ICollection<Inventory>? Inventories { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }

}