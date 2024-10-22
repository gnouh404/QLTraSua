using Microsoft.AspNetCore.Mvc;

namespace QLTraSua.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }

    }
}
