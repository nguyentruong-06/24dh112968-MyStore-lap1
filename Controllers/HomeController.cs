using _24dh112968_MyStore_lap1.Models;
using _24dh112968_MyStore_lap1.Models.ViewModle;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace _24dh112968_MyStore_lap1.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyStoreEntities db = new MyStoreEntities();

        //  Trang chủ - có hỗ trợ tìm kiếm và phân trang
        public ActionResult Index(string searchTerm, int? page)
        {
            var model = new HomeProductVM();
            var products = db.Products.AsQueryable();

            // --- Tìm kiếm sản phẩm ---
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model.SearchTerm = searchTerm;
                products = products.Where(p =>
                    p.ProductName.Contains(searchTerm) ||
                    p.ProductDescription.Contains(searchTerm) ||
                    p.Category.CategoryName.Contains(searchTerm));
            }

            // --- Phân trang ---
            int pageNumber = page ?? 1;
            int pageSize = 6;

            // --- Top 10 sản phẩm bán chạy ---
            model.FeaturedProducts = products
                .OrderByDescending(p => p.OrderDetails.Count())
                .Take(10)
                .ToList();

            // --- Sản phẩm mới / ít bán nhất ---
            model.NewProducts = products
                .OrderBy(p => p.OrderDetails.Count())
                .ToPagedList(pageNumber, pageSize);

            return View(model);
        }

        //  Trang giới thiệu
        public ActionResult About()
        {
            ViewBag.Message = "Giới thiệu về cửa hàng MyStore.";
            return View();
        }

        //  Trang liên hệ
        public ActionResult Contact()
        {
            ViewBag.Message = "Liên hệ với chúng tôi qua thông tin dưới đây.";
            return View();
        }





        // GET: Home/ProductDetails/5
        public ActionResult ProductDetails(int? id, int? quantity, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product pro = db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }

            // Lấy tất cả các sản phẩm cùng danh mục
            var products = db.Products.Where(p => p.CategoryID == pro.CategoryID && p.ProductID != pro.ProductID).AsQueryable();

            ProductDetailsVM model = new ProductDetailsVM();

            // Đoạn code liên quan tới phân trang
            // Lấy số trang hiện tại (mặc định là trang 1 nếu không có giá trị)
            int pageNumber = page ?? 1;
            int pageSize = model.pageSize; // Số sản phẩm mỗi trang

            model.product = pro;
            model.RelatedProducts = products.OrderBy(p => p.ProductID).Take(8).ToPagedList(pageNumber, pageSize);
            model.TopProducts = products.OrderByDescending(p => p.OrderDetails.Count()).Take(8).ToPagedList(pageNumber, pageSize);

            if (quantity.HasValue)
            {
                model.quantity = quantity.Value;
            }

            return View(model);
        }
    }
}
