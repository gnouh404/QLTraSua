using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTraSua.Data;
using QLTraSua.Models;

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
        [HttpGet]
        public IActionResult Payment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Payment(DeliveryInfo model)
        {
            if (ModelState.IsValid)
            {

                _context.DeliveryInfos.Add(model);
                _context.SaveChanges();


                // Return the success view with the model data
                return View("SuccessPayment", model);
            }

            // Nếu ModelState không hợp lệ, trả về lại trang Payment để người dùng sửa
            return View("Payment");
        }
        //[HttpPost]
        //public IActionResult Payment(DeliveryInfo model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Truy xuất dữ liệu giỏ hàng (giả định là dữ liệu này được truyền từ client dưới dạng JSON)
        //        var cartData = Request.Form["CartData"];
        //        var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(cartData);

        //        // Tạo một đơn hàng mới
        //        var order = new Order
        //        {
        //            OrderDate = DateTime.Now,
        //            PaymentMethod = model.PaymentType,
        //            TotalAmount = cartItems.Sum(item => item.Price * item.Quantity),
        //            OrderDetails = cartItems.Select(item => new OrderDetails
        //            {
        //                ProductName = item.Name,
        //                Quantity = item.Quantity,
        //                Price = item.Price,
        //                Customization = $"{item.Sugar}% đường, {item.Ice}% đá"
        //            }).ToList()
        //        };

        //        // Lưu đơn hàng vào cơ sở dữ liệu (sử dụng QLTraSuaContext)
        //        _context.Orders.Add(order);
        //        _context.SaveChanges();

        //        // Điều hướng hoặc hiển thị thông báo thành công
        //        return RedirectToAction("Success"); // Tùy chỉnh theo yêu cầu
        //    }

        //    return View(model);
        //}
    }
}
