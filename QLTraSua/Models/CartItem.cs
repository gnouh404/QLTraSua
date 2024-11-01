namespace QLTraSua.Models
{
    public class CartItem
    {
        public string Name { get; set; }       // Product name
        public decimal Price { get; set; }     // Product price
        public int Quantity { get; set; }      // Quantity ordered
        public string Sugar { get; set; }         // Sugar level customization
        public string Ice { get; set; }
        public string Size { get; set; }  // Ice level customization
    }
}