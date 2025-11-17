using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _24dh112968_MyStore_lap1.Models.ViewModle
{
    public class CheckoutVM
    {
        // Thông tin sản phẩm trong giỏ
        public List<CartItem> CartItems { get; set; }

        // Tổng tiền
        public decimal TotalAmount { get; set; }

        // Thông tin khách hàng
        public string FullName { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public string DeliveryMethod { get; set; }
    }

    


















    //public List<CartItem> CartItems { get; set; }
    //public int CustomerID { get; set; }
    //[Display(Name = "Ngày đặt hàng")]
    //public System.DateTime OrderDate { get; set; }

    //[Display(Name = "Tổng giá trị")]
    //public decimal TotalPrice { get; set; }


    //[Display(Name = "Trạng thái thanh toán")]
    //public string PaymentStatus { get; set; }

    //[Display(Name = "Phương thức thanh toán")]
    //public string PaymentMethod { get; set; }

    //[Display(Name = "Phương thức giao hàng")]
    //public string DeliveryMethod { get; set; }

    //[Display(Name = "Địa chỉ giao hàng")]
    //public string ShippingAddress { get; set; }

    //public string Username { get; set; }

    //public List<OrderDetail> OrderDetails { get; set; }
}
