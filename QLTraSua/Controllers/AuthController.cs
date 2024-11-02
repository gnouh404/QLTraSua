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
                return View(); // Trả về view đăng nhập
            }
            else
            {
                return RedirectToAction("Index", "Test"); // Đã đăng nhập, chuyển hướng đến trang chính
            }
        }

        [HttpPost]

        [HttpPost]
        public IActionResult Login(User user)
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                // Tìm người dùng trong cơ sở dữ liệu
                var u = db.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
                if (u != null)
                {
                    // Thiết lập thông tin người dùng trong Session
                    HttpContext.Session.SetString("UserName", u.Username);
                    HttpContext.Session.SetString("UserRole", u.Role);

                    System.Diagnostics.Debug.WriteLine($"User Role: {u.Role}");
                    // Điều hướng theo vai trò của người dùng
                    if (u.Role == "Admin")
                    {
                        return RedirectToAction("Index", "TestAdmin"); // Điều hướng đến trang quản trị
                    }
                    else if (u.Role == "User")
                    {
                        return RedirectToAction("Index", "Test"); // Điều hướng đến trang của người dùng thông thường
                    }
                    else
                    {
                        // Nếu vai trò không xác định
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    // Nếu thông tin đăng nhập không đúng
                    ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";

                    // Resetting user input fields
                    user.Username = string.Empty; // Clear username
                    user.Password = string.Empty; // Clear password
                }
            }

            return View(user); // Return the user model to keep input values
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
                    Password = model.Password ,// Lưu mật khẩu dưới dạng văn bản thuần, bạn nên mã hóa mật khẩu thực tế

					  Role = model.Role ?? "User"

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


