using _24dh112968_MyStore_lap1.Models;
using _24dh112968_MyStore_lap1.Models.ViewModle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _24dh112968_MyStore_lap1.Controllers
{
    public class AccountController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                //kiểm tra xem tên đăng nhập đã tồn tại chưa
                var existingUser = db.Users.SingleOrDefault(u => u.Username == model.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập này đã tồn tại!");
                    return View(model);
                }

                //nếu chưa tồn tại thì tạo ghi thông tin tài khoản trong bảng User
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password, // Lưu ý: Nên mã hóa mật khẩu trước khi lưu
                    UserRole = "c"
                };
                db.Users.Add(user);

                // và tạo bản ghi thông tin khách hàng trong bảng Customer
                var customer = new Customer
                {
                    CustomerName = model.CustomerName,
                    CustomerEmail = model.CustomerEmail,
                    CustomerPhone = model.CustomerPhone,
                    CustomerAddress = model.CustomerAddress,
                    Username = model.Username,
                };
                db.Customers.Add(customer);

                // lưu thông tin tài khoản và thông tin khách hàng vào CSDL
                db.SaveChanges();

                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }


        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Username == model.Username
                                                        && u.Password == model.Password
                                                        && u.UserRole == "c");
                if (user != null)
                {
                    // Lưu trạng thái đăng nhập vào session
                    Session["Username"] = user.Username;
                    Session["UserRole"] = user.UserRole;

                    // Lưu thông tin xác thực người dùng vào cookie
                    FormsAuthentication.SetAuthCookie(user.Username, true);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }

            return View(model);
        }
        public ActionResult ProfileInfo()
        {
            // Get the current logged-in user's username
            string username = User.Identity.Name;

            // Retrieve the customer information
            var customerInfo = db.Customers.FirstOrDefault(c => c.Username == username);

            if (customerInfo == null)
            {
                return HttpNotFound();
            }

            return View(customerInfo );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateContact(_24dh112968_MyStore_lap1.Models.Customer model)
        {
            if (ModelState.IsValid)
            {
                string username = User.Identity.Name;
                var customer = db.Customers.FirstOrDefault(c => c.Username == username);

                if (customer != null)
                {
                    customer.CustomerPhone = model.CustomerPhone;
                    customer.CustomerEmail = model.CustomerEmail;
                    customer.CustomerAddress = model.CustomerAddress;

                    db.SaveChanges();
                    TempData["Message"] = "Contact information updated successfully.";
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Logout()
        {
            // Hủy cookie xác thực
            FormsAuthentication.SignOut();

            // Xóa session
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();

            // Quay về trang Login
            return RedirectToAction("Login", "Account");
        }




    }

}