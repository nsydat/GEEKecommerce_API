using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GEEKecommerce_API.Data;
using GEEKecommerce_API.Models;
using GEEKecommerce_API.DTO;

namespace GEEKecommerce_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            if (request == null || request.OrderDetails == null || !request.OrderDetails.Any())
                return BadRequest("Invalid order data.");

            // Tạo đơn hàng mới
            var order = new Order
            {
                UserId = request.UserId,
                OrderDate = DateTime.UtcNow,
                TotalPrice = request.OrderDetails.Sum(d => d.Quantity * d.Price),
                Status = "Pending"
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Thêm chi tiết đơn hàng
            foreach (var item in request.OrderDetails)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                };
                _context.OrderDetails.Add(orderDetail);
            }
            await _context.SaveChangesAsync();

            // Xử lý thanh toán
            if (!ProcessPayment(order.TotalPrice))
            {
                order.Status = "Failed";
                await _context.SaveChangesAsync();
                return BadRequest(new { OrderId = order.Id, Message = "Payment failed." });
            }

            order.Status = "Completed";
            await _context.SaveChangesAsync();

            _ = Task.Run(() => SendOrderConfirmationEmail(order));

            return Ok(new
            {
                OrderId = order.Id,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                Message = "Order created and payment processed successfully."
            });
        }

        [HttpPost("{orderId}/confirm-email")]
        public async Task<IActionResult> SendOrderEmail(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
                return NotFound($"Order with ID {orderId} not found.");

            _ = Task.Run(() => SendOrderConfirmationEmail(order));

            return Accepted(new
            {
                OrderId = order.Id,
                Message = "Order confirmation email is being processed."
            });
        }

        private bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing payment of: {amount:C}");
            return true; 
        }

        private void SendOrderConfirmationEmail(Order order)
        {
            Console.WriteLine($"[Email Service] Confirmation email sent for Order ID: {order.Id}");
            Console.WriteLine($"User ID: {order.UserId}, Total Price: {order.TotalPrice}, Status: {order.Status}");
        }
    }
}
