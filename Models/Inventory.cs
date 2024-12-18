namespace GEEKecommerce_API.Models 
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int Quantity { get; set; }

        // Navigation properties
        public Product? Product { get; set; }
        public Store? Store { get; set; }
    }

}