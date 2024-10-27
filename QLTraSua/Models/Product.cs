using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTraSua.Models
{
    [Table("products")]
    public class Product
    {
        public Product()
        {
            ProductCarts = new List<ProductCart>();
            OrderDetails = new List<OrderDetails>();
        }

        [Key]
        [Column("product_id")]
        public int ProductID { get; set; }

        [Column("product_name")]
        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; }

        [Column("price")]
        [Display(Name = "Giá")]
        public decimal ProductPrice { get; set; }

        [Column("image_url")]
        public string? ImageUrl { get; set; }


        [ForeignKey("Category")]
        [Column("category_id")]
        [Display(Name = "Thể loại")]
        public int CategoryID { get; set; }


        public virtual Category? Category { get; set; }

        public virtual ICollection<ProductCart> ProductCarts { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}