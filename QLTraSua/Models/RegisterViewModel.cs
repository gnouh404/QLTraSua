using System.ComponentModel.DataAnnotations;

namespace QLTraSua.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Số điện thoại phải chứa từ 10 đến 11 chữ số.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail\.[A-Za-z]{2,3}(\.[A-Za-z]{2})?$", ErrorMessage = "Vui lòng nhập địa chỉ email Gmail hợp lệ")]
        public string Email { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập tên tài khoản")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập tên của bạn")]
		  public string? FirstName { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập họ của bạn")]
		public string? LastName { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
		[DataType(DataType.Password)]
		[RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
				ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự, bao gồm ít nhất một chữ hoa, một chữ số và một ký tự đặc biệt.")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp")]
		public string ConfirmPassword { get; set; }
	}
}
