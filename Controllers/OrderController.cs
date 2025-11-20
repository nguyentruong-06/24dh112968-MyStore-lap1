using _24dh112968_MyStore_lap1.Models;
using _24dh112968_MyStore_lap1.Models.ViewModle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _24dh112968_MyStore_lap1.Controllers
{
    public class OrderController : Controller
    {


        private MyStoreEntities db = new MyStoreEntities();

        // Lấy CartService giống trong CartController
        private CartService GetCartService()
        {
            return new CartService(Session);
        }

        // ============================
        // GET: /Order/Checkout
        // ============================
        public ActionResult Checkout()
        {
            var cart = GetCartService().GetCart().Items;

            if (cart == null || !cart.Any())
                return RedirectToAction("Index", "Cart");

            var model = new CheckoutVM
            {
                CartItems = cart.Select(x => new CartItem
                {
                    ProductID = x.ProductID,
                    ProductName = x.ProductName,
                    ProductImage = x.ProductImage,
                    UnitPrice = x.UnitPrice,
                    Quantity = x.Quantity
                }).ToList(),

                TotalAmount = cart.Sum(x => x.UnitPrice * x.Quantity)
            };

            return View(model);
        }

        // ============================
        // POST: /Order/Checkout
        // ============================
        [HttpPost]
        public ActionResult Checkout(CheckoutVM model)
        {
            var cart = GetCartService().GetCart().Items;

            if (cart == null || !cart.Any())
            {
                ModelState.AddModelError("", "Giỏ hàng trống!");
                return View(model);
            }

            // ---- Tạo đơn hàng ----
            Order order = new Order
            {
                CustomerID = 1, // Tạm gán, sau này lấy từ user login
                OrderDate = DateTime.Now,
                TotalAmount = cart.Sum(x => x.UnitPrice * x.Quantity),
                PaymentStatus = "Chờ xử lý",
                ShippingAddress = model.ShippingAddress,
                PaymentMethod = model.PaymentMethod,
                DeliveryMethod = model.DeliveryMethod
            };

            db.Orders.Add(order);
            db.SaveChanges(); // lưu trước để lấy OrderID

            // ---- Thêm chi tiết đơn hàng ----
            foreach (var item in cart)
            {
                OrderDetail detail = new OrderDetail
                {
                    OrderID = order.OrderID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice
                };
                db.OrderDetails.Add(detail);
            }

            db.SaveChanges();

            // ---- Xóa giỏ hàng ----
            GetCartService().ClearCart();

            return RedirectToAction("Success", new { id = order.OrderID });
        }

        // ============================
        // GET: /Order/Success
        // ============================
        public ActionResult Success(int id)
        {
            var order = db.Orders.Find(id);
            if (order == null) return HttpNotFound();

            return View(order);
        }

        public ActionResult OrderHistory()
        {
            int customerId = 1; // Tạm thời, sau này lấy từ user login

            // Lấy danh sách đơn hàng của khách hàng
            var orders = db.Orders
                           .Where(o => o.CustomerID == customerId)
                           .OrderByDescending(o => o.OrderDate)
                           .ToList();

            // Tạo ViewModel để hiển thị chi tiết mỗi đơn hàng
            var model = orders.Select(o => new OrderHistoryVM
            {
                OrderID = o.OrderID,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                PaymentStatus = o.PaymentStatus,
                DeliveryMethod = o.DeliveryMethod
            }).ToList();

            return View(model);
        }


        //// GET: Order
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}