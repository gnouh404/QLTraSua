using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTraSua.Data;

namespace QLTraSua.Controllers
{
    public class TestAdminController : Controller
    {
        private QLTraSuaContext db;

        public TestAdminController(QLTraSuaContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).ToList();
            return View(products);
        }
    }
}
