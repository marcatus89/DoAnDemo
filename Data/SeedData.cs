using Microsoft.EntityFrameworkCore;
using DoAnTotNghiep.Models;
using System.Linq;

namespace DoAnTotNghiep.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Chỉ chạy nếu cả hai bảng Categories và Products đều trống
            if (context.Categories.Any() || context.Products.Any())
            {
                return;
            }

            // Tạo và lưu Categories trước để có ID
            var categories = new Category[]
            {
                new Category { Name = "Bồn cầu" },
                new Category { Name = "Vòi sen" },
                new Category { Name = "Lavabo" }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges(); // Lưu để EF gán ID cho categories

            // Bây giờ mới tạo Products với CategoryId đã có
            var products = new Product[]
            {
                new Product { Name = "Bồn cầu thông minh A1", Price = 5000000M, CategoryId = categories[0].Id, ImageUrl = "https://placehold.co/600x400/cccccc/333333?text=Bon+Cau+A1" },
                new Product { Name = "Vòi sen tắm nhiệt đới B2", Price = 2500000M, CategoryId = categories[1].Id, ImageUrl = "https://placehold.co/600x400/cccccc/333333?text=Voi+Sen+B2" },
                new Product { Name = "Lavabo sứ cao cấp C3", Price = 3000000M, CategoryId = categories[2].Id, ImageUrl = "https://placehold.co/600x400/cccccc/333333?text=Lavabo+C3" }
            };
            
            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}

