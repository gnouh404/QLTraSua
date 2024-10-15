using System.ComponentModel.DataAnnotations.Schema;

namespace QLTraSua.Models
{
    [Table("product_topping")]
    public class ProductTopping
    {
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public int ToppingID { get; set; }
        public virtual Topping Topping { get; set; }


    }
}
