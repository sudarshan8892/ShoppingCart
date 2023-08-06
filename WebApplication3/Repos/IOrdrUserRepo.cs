using WebApplication3.ModelForView;
using WebApplication3.Models;

namespace WebApplication3
{
    public interface IOrdrUserRepo
    {
        List<ShoppingCartInfo> UserOrder(int userId);
    }
}