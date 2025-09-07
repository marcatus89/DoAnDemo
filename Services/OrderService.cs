using System;
using System.Linq;
using System.Threading.Tasks;
using DoAnTotNghiep.Data;
using DoAnTotNghiep.Models;

namespace DoAnTotNghiep.Services
{
    public class OrderService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly CartService _cartService;

        public OrderService(ApplicationDbContext dbContext, CartService cartService)
        {
            _dbContext = dbContext;
            _cartService = cartService;
        }

        public async Task<bool> PlaceOrderAsync(Order order)
        {
            var cartItems = _cartService.Items;
            if (!cartItems.Any())
            {
                return false; 
            }

            order.OrderDate = DateTime.Now;
            order.TotalAmount = _cartService.Total;
            
            // Gán trạng thái mặc định cho đơn hàng mới
            order.Status = "Chờ xác nhận";

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName ?? string.Empty,
                    Quantity = item.Quantity,
                    Price = item.Price
                };
                order.OrderDetails.Add(orderDetail);
            }

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            _cartService.ClearCart();

            return true;
        }
    }
}
