using System.ComponentModel.DataAnnotations.Schema;

namespace QLTraSua.Models
{
    [Table("order_details")]
    public class OrderDetails
    {
        [ForeignKey("Order")]
        [Column("order_id")]
        public int OrderID { get; set; }

        public virtual Order Order { get; set; }

        [Column("product_id")]
        public int ProductID { get; set; }

        public virtual Product Product { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }
    }
}
