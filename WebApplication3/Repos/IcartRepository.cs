using WebApplication3.ModelForView;
using WebApplication3.Models;

namespace WebApplication3
{
    public interface IcartRepository
    {
        public   int RemoveItem(int bookid, int UserId);
        int Addcart(int bookid, int qty, int UserId);
        public List<ShoppingCartInfo> GetUserCart(int UserId);
        int GetCartCount(int UserID);
        bool checkOut(int UserId);
    }
}