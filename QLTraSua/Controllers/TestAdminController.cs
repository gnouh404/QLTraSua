using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTraSua.Data;
using QLTraSua.Models;
using QLTraSua.ViewModels;
using System.Diagnostics;

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




        public IActionResult Duyet()
        {
            var orders = db.Orders.Include(o => o.User)
                                  .Include(o => o.OrderDetails) // Nếu cần thiết
                                  .Where(o => o.OrderDetails.Any()) // Lọc đơn hàng có sản phẩm
                                  .Select(o => new DeleteOrderViewModel
                                  {
                                      OrderID = o.OrderID,
                                      OrderDate = o.OrderDate,
                                      TotalAmount = o.TotalAmount,
                                      Status = o.Status,
                                      UserName = o.User != null
                                          ? $"{o.User.FirstName} {o.User.LastName}"
                                          : "Khách hàng không xác định",
                                      ProductName = o.OrderDetails.Select(od => od.Product.ProductName).FirstOrDefault() // Lấy tên sản phẩm đầu tiên
                                  })
                                  .ToList();

            return View(orders);
        }



        [Route(" DeleteOrder")]
        [HttpGet]
        public IActionResult DeleteOrder(int id)
        {
            Debug.WriteLine($"Product ID: {id}");

            var product1 = db.Orders.FirstOrDefault(o => o.OrderID == id);
            Debug.WriteLine($"Product ID: {product1.UserID}");
            if (product1 != null)
            {
                product1.Status = false;

            }
            db.SaveChanges();
            return RedirectToAction("Duyet");


        }
        [Route("AprroveOrder")]
        [HttpGet]
        public IActionResult AprroveOrder(int id)
        {
            Debug.WriteLine($"Product ID: {id}");

            var product1 = db.Orders.FirstOrDefault(o => o.OrderID == id);
            Debug.WriteLine($"Product ID: {product1.UserID}");
            if (product1 != null)
            {
                product1.Status = true;

            }
            db.SaveChanges();
            return RedirectToAction("Duyet");

        }

        //// POST: Learner/Delete/5
        [HttpPost, ActionName("DeleteOrder")]
        public IActionResult DeleteOrderConfirmed(int OrderID)
        {
            Debug.WriteLine($"Xóa đơn hàng với ID: {OrderID}");

            // Kiểm tra xem db.Orders có phải là null không
            if (db.Orders == null)
            {
                return Problem("Entity set 'Orders' is null.");
            }

            // Tìm đơn hàng theo ID
            var order = db.Orders.Find(OrderID);
            Debug.WriteLine($"Tìm kiếm đơn hàng với ID: {OrderID}. Kết quả: {(order != null ? "Tìm thấy" : "Không tìm thấy")}.");

            if (order != null)
            {
                // Xóa đơn hàng
                db.Orders.Remove(order);
                Debug.WriteLine($"Đơn hàng với ID: {OrderID} đã được xóa.");
                db.SaveChanges(); // Di chuyển vào trong điều kiện
            }
            else
            {
                Debug.WriteLine($"Không thể xóa đơn hàng với ID: {OrderID} vì không tìm thấy.");
            }

            // Chuyển hướng đến phương thức Duyet
            return RedirectToAction(nameof(Duyet));
        }




        public IActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductName, ProductPrice,CategoryID,ImageUrl")]
        Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }
        //Edit
        public IActionResult Edit(int id)
        {
            if (id == null || db.Products == null)
            {
                return NotFound();
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,
                [Bind("ProductID, ProductName, ProductPrice,CategoryID,ImageUrl")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(product); db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }
        private bool ProductExists(int id)
        {
            return (db.Products?.Any(e => e.ProductID == id)).GetValueOrDefault();
        }
        //Delete
        public IActionResult Delete(int id)
        {
            if (id == null || db.Products == null)
            {
                return NotFound();
            }
            var product = db.Products.Include(l => l.Category)
                .FirstOrDefault(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // POST: Learner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {


            if (db.Products == null)
            {
                return Problem("Entity set 'Products' is null.");
            }
            var product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
            }
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Dashboard()
        {
            var products = db.Products.Include(p => p.Category).ToList();
            return View(products);
        }
        public IActionResult Customer()
        {
            // Lấy danh sách khách hàng từ cơ sở dữ liệu
            var customers = db.Customers.ToList(); // Không cần bao gồm Category vì Customers không có thuộc tính Category
            return View(customers);
        }
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCustomer([Bind("CustomerID, CustomerName, Phone, Email, Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction(nameof(Customer));
            }

            ViewBag.CustomerData = new SelectList(db.Customers, "CustomerName");
            return View(customer);

        }
        public IActionResult EditCustomer(int id)
        {
            // Kiểm tra xem db.Customers có null không
            if (db.Customers == null)
            {
                return Problem("Entity set 'db.Customers' is null.");
            }

            // Tìm khách hàng theo CustomerID
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy khách hàng
            }

            // Thiết lập danh sách Category (nếu cần thiết) cho ViewBag
            // Nếu bạn có danh sách các loại khách hàng
            // ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", customer.CategoryID);

            return View(customer); // Trả về view với thông tin khách hàng
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCustomer(int id, [Bind("CustomerID, CustomerName, Phone, Email, Address")] Customer customer)
        {
            // Kiểm tra xem ID trong route có trùng với ID của khách hàng không
            if (id != customer.CustomerID)
            {
                return NotFound(); // Trả về 404 nếu ID không khớp
            }

            // Kiểm tra tính hợp lệ của Model
            if (ModelState.IsValid)
            {
                try
                {
                    // Cập nhật thông tin khách hàng
                    db.Entry(customer).State = EntityState.Modified; // Đánh dấu khách hàng là đã thay đổi
                    db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerID))
                    {
                        return NotFound(); // Trả về 404 nếu khách hàng không tồn tại
                    }
                    else
                    {
                        throw; // Ném lại ngoại lệ nếu có lỗi khác
                    }
                }
                return RedirectToAction(nameof(Customer)); // Chuyển hướng về trang danh sách khách hàng
            }
            return View(customer); // Trả về lại view với thông tin khách hàng đã nhập
        }
        private bool CustomerExists(int id)
        {
            return db.Customers?.Any(e => e.CustomerID == id) ?? false; // Trả về true nếu khách hàng tồn tại
        }
        public IActionResult DeleteCustomer(int id)
        {
            // Kiểm tra nếu db.Customers là null
            if (db.Customers == null)
            {
                return NotFound();
            }

            // Tìm khách hàng theo CustomerID
            var customer = db.Customers.FirstOrDefault(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy khách hàng
            }

            return View(customer); // Trả về view với thông tin khách hàng
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("DeleteCustomer")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCustomerConfirmed(int id)
        {
            // Kiểm tra nếu db.Customers là null
            if (db.Customers == null)
            {
                return Problem("Entity set 'Customers' is null.");
            }

            // Tìm và xóa khách hàng khỏi cơ sở dữ liệu
            var customer = db.Customers.Find(id);
            if (customer != null)
            {
                db.Customers.Remove(customer); // Xóa khách hàng
            }

            db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            return RedirectToAction(nameof(Customer)); // Chuyển hướng về trang danh sách khách hàng
        }
    }
}
