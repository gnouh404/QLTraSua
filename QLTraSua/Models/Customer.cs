using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTraSua.Models
{
    [Table("CUSTOMERS")]
    public class Customer
    {
        public Customer()
        {
            ProductCarts = new List<ProductCart>();
            Orders = new List<Order>();
        }

        [Key]
        [Column("CustomerID")]  // Đảm bảo tên cột khớp với tên cột trong cơ sở dữ liệu
        public int CustomerID { get; set; }

        [Column("CustomerName")]  // Tên cột đã thay đổi
        [Display(Name = "Tên khách hàng")]
        public string CustomerName { get; set; }  // Đổi từ FirstName và LastName thành CustomerName

        [Column("Phone")]  // Tên cột đã thay đổi
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }  // Giữ nguyên tên thuộc tính

        [Column("Email")]  // Tên cột đã thay đổi
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Column("Address")]  // Tên cột đã thay đổi
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        public virtual ICollection<ProductCart> ProductCarts { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
