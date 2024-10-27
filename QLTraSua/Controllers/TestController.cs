using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTraSua.Data;

namespace QLTraSua.Controllers
{
    public class TestController : Controller
    {
        private QLTraSuaContext _context;
        public TestController(QLTraSuaContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .ToList();
            return View(products);
        }

        public IActionResult Payment()
        {
            return View();
        }

    }
}
