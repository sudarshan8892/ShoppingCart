using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using WebApplication3.ModelForView;
using WebApplication3.Models;

namespace WebApplication3.Repos
{
    public class OrderUserRepo : IOrdrUserRepo
    {
        private readonly Shopping_cartContext _context;

        public OrderUserRepo(Shopping_cartContext orderUser)
        {
            _context = orderUser;
        }

        public List<ShoppingCartInfo> UserOrder(int userId)
        {
            userId = 1;
            var shoppingCartInfoList = (from order in _context.Orders
                                        join orderDetail in _context.Orderdetails on order.Id equals orderDetail.OrderId
                                        join book in _context.Books on orderDetail.BookId equals book.Id
                                        join gener in _context.Genres on book.GenerId equals gener.Id
                                        join orderStatus in _context.OrderStatuses on order.OrderStatusId equals orderStatus.Id
                                        where order.UserId == userId
                                        select new ShoppingCartInfo
                                        {
                                            status=orderStatus,
                                            orderdetail = orderDetail,
                                            orders = order,
                                            Booking = book,
                                            Gener = gener
                                        }).ToList();


            return shoppingCartInfoList;
        }


    }
}
