using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTraSua.Models
{
    [Table("cart")]
    public class Cart
    {
        public Cart() { 
            User = new User();
            ProductCarts = new List<ProductCart>();
        }

        [Key]
        [Column("cart_id")]
        public int CartID { get; set; }

        [Column("cart_quantity")]
        public int CartQuantity { get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserID { get; set; }

        public virtual User User { get; set; } 
        
        public virtual ICollection<ProductCart> ProductCarts { get; set; }

    }
}
