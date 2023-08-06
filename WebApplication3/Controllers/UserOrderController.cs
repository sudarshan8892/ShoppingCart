using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    public class UserOrderController : Controller
    {
        private readonly IOrdrUserRepo _ordrUserRepo;
        public UserOrderController(IOrdrUserRepo ordrUserRepo)
        {
                _ordrUserRepo  = ordrUserRepo;  
        }
        public IActionResult UserOrder( int userId )
        {
            var orders = _ordrUserRepo.UserOrder(userId);
            return View(orders);
        }
    }
}
