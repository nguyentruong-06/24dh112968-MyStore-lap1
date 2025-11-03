using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _24dh112968_MyStore_lap1.Models.ViewModle
{
    public class CartItem // lưu thông tin 
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductImage { get; set; }
        // Tổng giá cho mỗi sản phẩm
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}