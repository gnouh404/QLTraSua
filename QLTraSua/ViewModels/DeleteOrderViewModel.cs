namespace QLTraSua.ViewModels
{
    public class DeleteOrderViewModel
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string UserName { get; set; } // Nếu bạn muốn hiển thị tên người dùng

        public string ProductName { get; set; }
        public bool? Status { get; set; }
    }

}
