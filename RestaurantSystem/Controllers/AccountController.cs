using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Models;
using Microsoft.AspNetCore.Http; // مهم عشان السيشن

namespace RestaurantSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly RestaurantDbContext _context;

        public AccountController(RestaurantDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // لو مفيش أدمن خالص، بنكريت واحد افتراضي
            if (!_context.Admins.Any())
            {
                var defaultAdmin = new Admin { Username = "admin", Password = "123" };
                _context.Admins.Add(defaultAdmin);
                _context.SaveChanges();
                ViewBag.Message = "Default Admin created (User: admin, Pass: 123)";
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(string emailOrUser, string password)
        {
            // 1. البحث في جدول الأدمن
            var admin = _context.Admins.FirstOrDefault(a => a.Username == emailOrUser && a.Password == password);
            if (admin != null)
            {
                // بنسجل بيانات الأدمن
                HttpContext.Session.SetInt32("UserID", admin.AdminId); // (لو فيه خطأ هنا غير AdminId لـ Id)
                HttpContext.Session.SetString("Role", "Admin");
                HttpContext.Session.SetString("Name", admin.Username);
                return RedirectToAction("Index", "Home");
            }

            // 2. البحث في جدول العملاء
            var customer = _context.Customers.FirstOrDefault(c => c.Email == emailOrUser && c.Password == password);
            if (customer != null)
            {
                // 👇👇👇 التعديل الضروري هنا 👇👇👇
                // بنحفظ رقم العميل في السيشن عشان السلة تستخدمه
                HttpContext.Session.SetInt32("UserID", customer.CustomerId);
                // ⚠️ ملحوظة: لو كلمة CustomerId تحتها خط أحمر، جرب تخليها Id أو ID

                HttpContext.Session.SetString("Role", "Customer");
                HttpContext.Session.SetString("Name", customer.Name);
                return RedirectToAction("Menu", "Home");
            }

            ViewBag.Error = "Invalid Username or Password";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (_context.Customers.Any(c => c.Email == customer.Email))
                {
                    ViewBag.Error = "This email is already registered!";
                    return View(customer);
                }

                _context.Customers.Add(customer);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(customer);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}