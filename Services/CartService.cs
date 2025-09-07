using System;
using System.Collections.Generic;
using System.Linq;
using DoAnTotNghiep.Models;

namespace DoAnTotNghiep.Services
{
    public class CartService
    {
        public event Action? OnChange;
        private List<CartItem> _items = new List<CartItem>();
        public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

        // --- NEW: Thêm thuộc tính tính tổng tiền ---
        public decimal Total => _items.Sum(item => item.Price * item.Quantity);

        public void AddToCart(Product product)
        {
            var existingItem = _items.FirstOrDefault(i => i.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                _items.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }
            NotifyStateChanged();
        }

        // --- NEW: Thêm phương thức cập nhật số lượng ---
        public void UpdateQuantity(int productId, int quantity)
        {
            var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                if (quantity > 0)
                {
                    existingItem.Quantity = quantity;
                }
                else
                {
                    // Nếu số lượng là 0, xóa sản phẩm khỏi giỏ
                    _items.Remove(existingItem);
                }
                NotifyStateChanged();
            }
        }

        // --- NEW: Thêm phương thức xóa sản phẩm ---
        public void RemoveFromCart(int productId)
        {
            var itemToRemove = _items.FirstOrDefault(i => i.ProductId == productId);
            if (itemToRemove != null)
            {
                _items.Remove(itemToRemove);
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
        public void ClearCart()
        {
            _items.Clear();
            NotifyStateChanged();
        }
    }
}