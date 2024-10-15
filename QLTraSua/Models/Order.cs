using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTraSua.Models
{
    [Table("orders")]
    public class Order
    {
        public Order() { 
            User = new User();
            Cart = new Cart();
            OrderDetails = new List<OrderDetails>();
        }

        [Key]
        [Column("order_id")]
        public int OrderID { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; }

        [Column("total_amount")]
        public int TotalAmount { get; set; }

        [Column("status")]
        public Boolean Status { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserID { get; set; }

        [ForeignKey("Cart")]
        [Column("cart_id")]
        public int CartID { get; set; }

        public virtual User User { get; set; }

        public virtual Cart Cart { get; set; }


        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
