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
			if (TempData.ContainsKey("SuccessMessage"))
			{
				ViewBag.SuccessMessage = TempData["SuccessMessage"];
			}
		
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
				}else
                 {
                    @ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng, vui lòng nhập lại";
					ModelState.Clear(); // Reset input
				}
			}
			return View();
		}

        public ActionResult Register()
        {
            return View();
        }


        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem số điện thoại, email, và tên đăng nhập đã tồn tại chưa
                var existingUser = db.Users
                    .FirstOrDefault(u => u.Email == model.Email ||
                                         u.Username == model.Username ||
                                         u.Phone == model.Phone);

                if (existingUser != null)
                {
                    if (existingUser.Email == model.Email)
                    {
                        ModelState.AddModelError("Email", "Email đã tồn tại.");
                    }
                    if (existingUser.Username == model.Username)
                    {
                        ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại.");
                    }
                    if (existingUser.Phone == model.Phone)
                    {
                        ModelState.AddModelError("Phone", "Số điện thoại đã tồn tại.");
                    }
                    return View(model);
                }

                // Tạo một người dùng mới từ RegisterViewModel
                var user = new User
                {
                    Phone = model.Phone,
                    Email = model.Email,
                    Username = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password // Lưu mật khẩu dưới dạng văn bản thuần, bạn nên mã hóa mật khẩu thực tế
                };

                db.Users.Add(user);
                db.SaveChanges(); // Lưu vào cơ sở dữ liệu

                TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                // Redirect đến trang thành công hoặc đăng nhập
                return RedirectToAction("Login");
            }

            // Nếu model không hợp lệ, trả về view với model để hiển thị lỗi
            return View(model);
        }

    }

}


