using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTraSua.Models
{
    [Table("products")]
    public class Product
    {
        public Product() { 
            ProductToppings = new List<ProductTopping>();
            ProductCarts = new List<ProductCart>();
            OrderDetails = new List<OrderDetails>();
        }
        [Key]
        [Column("product_id")]
        public int ProductID;

        [Column("product_name")]
        
        public string ProductName {  get; set; }

        [Column("price")]
        public double ProductPrice { get; set; }

        [Column("image_url")]
        public string ImageUrl { get; set; }


        [ForeignKey("Category")]
        [Column("category_id")]
        public int CategoryID { get; set; }


        public virtual Category Category { get; set; }

        public virtual ICollection<ProductTopping> ProductToppings { get; set; }

        public virtual ICollection<ProductCart> ProductCarts { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
