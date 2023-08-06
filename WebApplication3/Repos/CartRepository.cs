using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebApplication3.Models;
using WebApplication3.ModelForView;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Razor.Language;
using Newtonsoft.Json.Linq;

namespace WebApplication3.Repos
{

    public class CartRepository : IcartRepository
    {
        private readonly Shopping_cartContext _CartContext;
        public CartRepository(Shopping_cartContext CartContext)
        {
            _CartContext = CartContext;
        }
        public int Addcart(int bookid, int qty, int UserId)
        {
            int userId = _CartContext.Users.Where(a => a.Id == UserId)
                   .Select(a => a.Id).SingleOrDefault();
            using var transaction = _CartContext.Database.BeginTransaction();
            try
            {

                if (userId == 0)
                {
                    return 0;
                }
                var cart = getcart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId

                    };
                    _CartContext.ShoppingCarts.Add(cart);

                }
                _CartContext.SaveChanges();


                var cartitem = _CartContext.
                    CartDetails.
                    FirstOrDefault(a => a.ShoppingId == cart.Id &&
                    a.BookingId == bookid);
                if (cartitem is not null)
                {
                    cartitem.Qunitity += qty;
                }
                else
                {
                    var book = _CartContext.Books.Find(bookid);
                    cartitem = new CartDetail
                    {

                        ShoppingId = cart.Id,
                        BookingId = bookid,
                        Qunitity = qty,
                        UnitPrice = book.Price


                };
                    _CartContext.CartDetails.Add(cartitem);
                }
                _CartContext.SaveChanges();
                transaction.Commit();

            }
            catch (Exception ex)
            {
                return 0;
            }
            var CartCount = GetCartCount(userId);
            return CartCount;
        }
        public int RemoveItem(int bookid, int UserId)
        {
            int userId = _CartContext.Users.Where(a => a.Id == UserId)
                   .Select(a => a.Id).SingleOrDefault();
            using var transaction = _CartContext.Database.BeginTransaction();
            try
            {

                if (userId == 0)
                {
                    return 0;
                }
                var cart = getcart(userId);
                if (cart is null)
                {

                    return 0;
                }
                //cart details
                var cartitem = _CartContext.
                    CartDetails.
                    FirstOrDefault(a => a.ShoppingId == cart.Id &&
                    a.BookingId == bookid);
                if (cartitem is null)
                {
                    return 0;
                }
                else if (cartitem.Qunitity > 1)
                {
                    cartitem.Qunitity = cartitem.Qunitity - 1;
                }
                else if (cartitem.Qunitity == 1)
                {
                    _CartContext.CartDetails.Remove(cartitem);
                }
                else if(cartitem.Qunitity == 0)
                {
                    _CartContext.CartDetails.Remove(cartitem);
                }
                _CartContext.SaveChanges();
                transaction.Commit();

            }
            catch (Exception ex)
            {
                return 0;
            }
            var CartCount = GetCartCount(userId);
            return CartCount;
        }
        private ShoppingCart getcart(int userId)
        {
            var cart = _CartContext.ShoppingCarts.FirstOrDefault(a => a.UserId == userId);
            return cart;
        }
        public List<ShoppingCartInfo> GetUserCart(int UserId = 1)
        {
            if (UserId == 0)
            {
                UserId = 1;
            }
            int userId = _CartContext.Users.Where(a => a.Id == UserId)
                    .Select(a => a.Id).SingleOrDefault();
            if (userId == 0)
            {
                throw new Exception("userID not exist");
            }
            //var shoppingcart = _CartContext.ShoppingCarts
            //    .Include(a => a.CartDetails)
            //    .ThenInclude(a => a.Booking)
            //    .ThenInclude(a => a.Gener)
            //    .Where(a => a.UserId == userId).ToList();

            //return shoppingcart;

            //linq with join
            var query = (from cart in _CartContext.ShoppingCarts
                         join cartDetail in _CartContext.CartDetails on cart.Id equals cartDetail.ShoppingId
                         join booking in _CartContext.Books on cartDetail.BookingId equals booking.Id
                         join gener in _CartContext.Genres on booking.GenerId equals gener.Id
                         where cart.UserId == userId
                         select new ShoppingCartInfo
                         {
                             ShoppingCart = cart,
                             CartDetail = cartDetail,
                             Booking = booking,
                             Gener = gener
                         }).ToList();
            return query;

        }
        public int GetCartCount(int userId)
        {
            var data = (from a in _CartContext.ShoppingCarts
                        join b in _CartContext.CartDetails
                        on a.Id equals b.ShoppingId
                        select new
                        {
                            b.Id
                        }).ToList();
            return data.Count;

        }

        public bool  checkOut(int UserId)//enter order,orderDetails--remove cartDetails
        {
            using var transction = _CartContext.Database.BeginTransaction( ); 
            try
            {
                if (UserId == 0)
                {
                    UserId = 1;
                }
                int userId = _CartContext.Users.Where(a => a.Id == UserId)
                        .Select(a => a.Id).SingleOrDefault();
                if (userId == 0)
                {
                    throw new Exception("userID not exist");
                }
                var getcartDetails = getcart(userId);
                if(getcartDetails is  null)
                {
                    throw new Exception("Invalid Cart");
                }
                var cartdetails = _CartContext.CartDetails
                    .Where(a => a.ShoppingId == getcartDetails.Id).ToList();
                if(cartdetails.Count==0)
                {
                    
                        throw new Exception("Cart is empty");
                }

                var order = new Order
                {
                    UserId = userId,
                    CreatedDate = DateAndTime.Now,
                    OrderStatusId = 1,
                };
                _CartContext.Orders.Add(order);
                _CartContext.SaveChanges();

                foreach(var  item in  cartdetails)
                {
                    var orderdetails = new Orderdetail
                    {
                        BookId = item.BookingId,
                        OrderId = order.Id,
                        Qunitity = item.Qunitity,
                        UnitPrice = item.UnitPrice
                        
                    };
                    _CartContext.Orderdetails.Add(orderdetails);

                }
                _CartContext.SaveChanges();
                _CartContext.CartDetails.RemoveRange(cartdetails);
                _CartContext.SaveChanges();
                transction.Commit();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
    }
}
