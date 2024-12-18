namespace GEEKecommerce_API.Models 
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";
        public decimal TotalPrice { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }

}