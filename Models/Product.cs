using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnTotNghiep.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }

        // Foreign key để liên kết với bảng Category
        public int? CategoryId { get; set; }

        // Navigation property để EF Core hiểu mối quan hệ
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}

