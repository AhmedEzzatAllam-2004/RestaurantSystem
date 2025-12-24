using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using RestaurantSystem.Models;
using Microsoft.AspNetCore.Http; 
using System.Linq;

namespace RestaurantSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestaurantDbContext _context;

        public HomeController(RestaurantDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                return View(); 
            }
            else if (HttpContext.Session.GetString("Role") == "Customer")
            {
                return RedirectToAction("Menu");
            }

            return RedirectToAction("Login", "Account");
        }


        public IActionResult Menu()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                return RedirectToAction("Login", "Account");
            }


            var items = _context.MenuItems
                                .Include(m => m.Category)
                                .Include(m => m.Branch)
                                .ToList();

            return View(items);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}