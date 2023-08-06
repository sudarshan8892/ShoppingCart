using Microsoft.AspNetCore.Mvc;

using WebApplication3.Models;
using System;
using WebApplication3.ModelForView;

namespace WebApplication3.Controllers
{
    public class CartController : Controller
    {
        private readonly IcartRepository _cartRepos;
        public CartController(IcartRepository icartRepository)
        {
            _cartRepos = icartRepository;
        }
        public IActionResult Addcart(int bookid, int UserId, int qty = 1, int redirect = 0)
        {
            var cartCount = _cartRepos.Addcart(bookid, UserId, qty);
            if (redirect == 0)
            {
                return Ok(cartCount);
            }
            return RedirectToAction("GetUserCart");
        }
        public IActionResult Removecart(int bookid, int UserId)
        {
            var cartcount = _cartRepos.RemoveItem(bookid, UserId);
            return RedirectToAction("GetUserCart");
        }
        public IActionResult GetUserCart(int UserId)
        {
            var cart = _cartRepos.GetUserCart(UserId);
           
            return View(cart);
        }
        public IActionResult GetCartCount(int UserID)
        {
            var cartitem = _cartRepos.GetCartCount(UserID);
            return Ok(cartitem);
        }
        public IActionResult DoCheckOut(int UserID)
        {
            bool ischeckOut= _cartRepos.checkOut(UserID);
            return RedirectToAction("Index", "Home");
        }
    }
}
