using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTraSua.Models
{
    [Table("delivery")]
    public class DeliveryInfo
    {
        [Key]
        [Column("delivery_id")]
        public int Id { get; set; }  // Đặt tên thuộc tính Id hoặc tên khác phù hợp, với kiểu dữ liệu phù hợp

        [Column("customer_name")]
        [Required(ErrorMessage = "Vui lòng nhập tên người nhận")]
        public string CustomerName { get; set; }

        [Column("customer_phone")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải có 10 chữ số")]
        public string CustomerPhone { get; set; }

        [Column("customer_location")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string CustomerLocation { get; set; }

        [Column("customer_note")]
        public string CustomerNote { get; set; }
    }



}
//public class DeliveryInfo
//{
//    [Key]
//    public int Id { get; set; }  // Đặt tên thuộc tính Id hoặc tên khác phù hợp, với kiểu dữ liệu phù hợp

//    [Required(ErrorMessage = "Vui lòng nhập tên người nhận")]
//    public string CustomerName { get; set; }

//    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
//    [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải có 10 chữ số")]
//    public string CustomerPhone { get; set; }

//    [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
//    public string CustomerLocation { get; set; }

//}
