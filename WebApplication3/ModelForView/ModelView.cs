using WebApplication3.Models;

namespace WebApplication3.ModelForView
{
    public class ModelView
    {
        public List<Book>books  { get; set; }
        public List<Genre> genres { get; set; }
        public int GenereID { get; set; } = 0;
        public string str { get; set; } = "";

        public  List<User> users { get; set; } 
        public List<CartDetail> cartDetail { get; set; }

        public List< ShoppingCart> shoppingCarts { get; set; }
    }
}
