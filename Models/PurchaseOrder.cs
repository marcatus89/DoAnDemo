using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoAnTotNghiep.Models
{
    // Model lưu thông tin một đơn nhập hàng
    public class PurchaseOrder
    {
        public int Id { get; set; }

        [Required]
        public int SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? ExpectedDeliveryDate { get; set; }
        public string Status { get; set; } = "Đã đặt hàng"; // Trạng thái: Đã đặt hàng, Đã nhận hàng

        public virtual ICollection<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();
    }
}
