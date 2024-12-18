namespace GEEKecommerce_API.DTO
{
    public class OrderRequest
    {
        public int UserId { get; set; }
        public List<OrderDetailRequest> OrderDetails { get; set; }
    }
}
