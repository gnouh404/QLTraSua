using Microsoft.AspNetCore.Mvc;

namespace QLTraSua.Controllers
{
	public class AuthController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}


		public IActionResult Login()
		{
			return View();
		}

		public ActionResult Register()
		{
			return View();
		}
	}
}
