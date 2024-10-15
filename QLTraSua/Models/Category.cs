using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTraSua.Models
{
    [Table("categories")]
    public class Category
    {
        public Category() { 
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("category_id")]
        public int CategoryID {  get; set; }

        [Column("category_name")]
        public string CategoryName { get; set; }   

        public virtual ICollection<Product> Products { get; set; }


    }
}
