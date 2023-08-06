using WebApplication3.Models;

namespace WebApplication3.ModelForView
{
    public class ShoppingCartInfo
    {
        public ShoppingCart ShoppingCart { get; set; }
        public CartDetail CartDetail { get; set; }
        public Book Booking { get; set; }
        public Genre Gener { get; set; }

        public Order orders { get; set; }

        public Orderdetail orderdetail { get; set; }
        public OrderStatus  status { get; set; }
    }
}
