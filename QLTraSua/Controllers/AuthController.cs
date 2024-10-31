using Microsoft.AspNetCore.Mvc;
using QLTraSua.Data;
using QLTraSua.Models;

namespace QLTraSua.Controllers
{
	public class AuthController : Controller
	{
		private QLTraSuaContext db;

		public AuthController(QLTraSuaContext context)
		{
			db = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Login()
		{
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return View();
			}
			else
			{
				return RedirectToAction("Index", "Test");
			}
		}
		[HttpPost]
		public IActionResult Login(User user)
		{
			if (HttpContext.Session.GetString("UserName") == null)
			{
				var u = db.Users.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
				if (u != null)
				{
					HttpContext.Session.SetString("UserName", u.Username.ToString());
					return RedirectToAction("Index", "Test");
				}
			}
			return View();
		}
	}
}

