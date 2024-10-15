using System.ComponentModel.DataAnnotations.Schema;

namespace QLTraSua.Models
{
    public class ProductCart
    {
        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductID { get; set; }

        public virtual Product Product { get; set; }

        [ForeignKey("Cart")]
        [Column("cart_id")]
        public int CartID { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
