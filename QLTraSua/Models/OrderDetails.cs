using System.ComponentModel.DataAnnotations.Schema;

namespace QLTraSua.Models
{
    [Table("order_details")]
    public class OrderDetails
    {

        public int OrderID { get; set; }

        public virtual Order Order { get; set; }

        public int ProductID { get; set; }

        public virtual Product Product { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }
    }
}
