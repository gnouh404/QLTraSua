using Microsoft.AspNetCore.Mvc;

namespace QLTraSua.Controllers
{
    public class TestAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
