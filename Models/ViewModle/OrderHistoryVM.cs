using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _24dh112968_MyStore_lap1.Models.ViewModle
{
    public class OrderHistoryVM : Controller
    {
        // GET: OrderHistoryVM
        
            public int OrderID { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal TotalAmount { get; set; }
            public string PaymentStatus { get; set; }
            public string DeliveryMethod { get; set; }
        

        public ActionResult Index()
        {
            return View();
        }
    }
}