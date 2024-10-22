using Microsoft.EntityFrameworkCore;
using QLTraSua.Models;

namespace QLTraSua.Data
{
    public class QLTraSuaContext : DbContext
    {
        public QLTraSuaContext(DbContextOptions<QLTraSuaContext> options):base(options) { }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Topping> Toppings { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<ProductTopping> ProductToppings { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ProductCart> ProductCarts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductTopping>()
                .HasKey(pt => new { pt.ProductID, pt.ToppingID });

        // config relationship between products and topping
            modelBuilder.Entity<ProductTopping>()
            .HasOne(pt => pt.Product)
            .WithMany(p => p.ProductToppings)
            .HasForeignKey(pt => pt.ProductID);

            modelBuilder.Entity<ProductTopping>()
                .HasOne(pt => pt.Topping)
                .WithMany(t => t.ProductToppings)
                .HasForeignKey(pt => pt.ToppingID);

        // products and categories
            modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)          
            .WithMany(c => c.Products)        
            .HasForeignKey(p => p.CategoryID);

        // products vs cart
            modelBuilder.Entity<ProductCart>()
        .HasKey(pc => new { pc.ProductID, pc.CartID }); // Khóa chính cho bảng trung gian

            modelBuilder.Entity<ProductCart>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCarts)
                .HasForeignKey(pc => pc.ProductID);

            modelBuilder.Entity<ProductCart>()
                .HasOne(pc => pc.Cart)
                .WithMany(c => c.ProductCarts)
                .HasForeignKey(pc => pc.CartID);

            // products vs order
            modelBuilder.Entity<OrderDetails>()
                .HasKey(pc => new { pc.ProductID, pc.OrderID });

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductID);

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID);

            // cart vs orders
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Cart)
                .WithMany() // Nếu không cần truy cập ngược lại từ Cart đến Order
                .HasForeignKey(o => o.CartID);

            // user vs order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserID);

            // user vs cart
            modelBuilder.Entity<User>()
            .HasOne(u => u.Cart)       // Mỗi User có một Cart
            .WithOne(c => c.User)     // Mỗi Cart thuộc về một User
            .HasForeignKey<Cart>(c => c.UserID);
        }
        public void Seed()
        {
            var categories = Categories.ToList();
            Categories.RemoveRange(categories);
            SaveChanges();

            if (!Categories.Any())
            {
                var newCategories = new List<Category>
            {
                new Category { CategoryName = "Món Nổi Bật" },
                new Category { CategoryName = "Instant Milk Tea" },
                new Category { CategoryName = "Trà Sữa" },
                new Category { CategoryName = "Fresh Fruit Tea" },
                new Category { CategoryName = "Macchiato Cream Cheese" },
                new Category { CategoryName = "Cà Phê" },
                new Category { CategoryName = "Ice Cream" },
                new Category { CategoryName = "Special Menu" }
            };

                Categories.AddRange(newCategories);
                SaveChanges();
            }
        }
    }
}
