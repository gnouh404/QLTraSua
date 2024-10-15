using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTraSua.Models
{
    [Table("topping")]
    public class Topping
    {
        public Topping() { 
            ProductToppings = new List<ProductTopping>();
        }

        [Key]
        [Column("topping_id")]
        public int ToppingID { get; set; }

        [Column("topping_name")]
        public string ToppingName { get; set; }

        [Column("price")]
        public decimal ToppingPrice {  get; set; }

        public virtual ICollection<ProductTopping> ProductToppings { get; set; }
    }
}
