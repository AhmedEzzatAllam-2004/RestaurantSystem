using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace RestaurantSystem.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly RestaurantDbContext _context;

        public ShoppingCartController(RestaurantDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Account");

            var cartItems = _context.ShoppingCarts
                                    .Include(c => c.MenuItem)
                                    .Where(c => c.CustomerId == userId)
                                    .ToList();

            ViewBag.Total = cartItems.Sum(c => c.Quantity * (c.MenuItem != null ? c.MenuItem.Price : 0));
            return View(cartItems);
        }

        // دالة الإضافة (معدلة لتسمح بأصناف متعددة)
        public IActionResult AddToCart(int itemId)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Account");

            // بنشوف هل العميل ده اختار *نفس الأكلة* دي قبل كده؟
            var existingItem = _context.ShoppingCarts
                                       .FirstOrDefault(c => c.CustomerId == userId && c.MenuItemId == itemId);

            if (existingItem != null)
            {
                // لو موجودة، زود العدد
                existingItem.Quantity++;
            }
            else
            {
                // لو مش موجودة (أكلة جديدة)، ضيف سطر جديد
                var cartItem = new ShoppingCart
                {
                    CustomerId = (int)userId,
                    MenuItemId = itemId,
                    Quantity = 1
                };
                _context.ShoppingCarts.Add(cartItem);
            }

            _context.SaveChanges();
            return RedirectToAction("Index"); // وديه للسلة عشان يشوف اللي انضاف
        }

        public IActionResult Remove(int id)
        {
            var item = _context.ShoppingCarts.Find(id);
            if (item != null)
            {
                _context.ShoppingCarts.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // دالة الشيك أوت (معدلة عشان تقبل التحويل)
        public IActionResult Checkout()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Account");

            var cartItems = _context.ShoppingCarts
                                    .Include(c => c.MenuItem)
                                    .Where(c => c.CustomerId == userId)
                                    .ToList();

            if (cartItems.Count == 0) return RedirectToAction("Index");

            var newOrder = new Order
            {
                CustomerId = (int)userId,
                OrderDate = DateTime.Now,
                TotalAmount = (decimal)cartItems.Sum(c => c.Quantity * (c.MenuItem != null ? c.MenuItem.Price : 0))
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = newOrder.OrderId,
                    // 👇 الحل السحري: (int) قبل الاسم عشان يجبره يتحول
                    ItemId = (int)item.MenuItemId,
                    Quantity = item.Quantity,
                    Price = item.MenuItem?.Price ?? 0
                };
                _context.OrderDetails.Add(orderDetail);
            }

            _context.ShoppingCarts.RemoveRange(cartItems);
            _context.SaveChanges();

            return RedirectToAction("OrderSuccess");
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}