using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTraSua.Data;
using QLTraSua.Models;

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
        //Create
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
    }
}
