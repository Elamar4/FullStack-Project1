using Bookly.DataAccess.Repository;
using Bookly.DataAccess.Repository.IRepository;
using Bookly.Models;
using Bookly.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bookly.Areas.Costumer.Controllers
{
    [Area("Costumer")]
    [Authorize]
  
    public class CartController : Controller
    {
        private readonly IUnitofWork _unitofwork;
        public AddtoCartVM AddtoCartVM { get; set; }

        public CartController(IUnitofWork unitofwork)
        {
            _unitofwork = unitofwork;

        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;


            AddtoCartVM = new()
            {
                shoppingCarts = _unitofwork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId,
                inculudeProperties: "Product"),
             
            };

            foreach(var cart in AddtoCartVM.shoppingCarts)
            {
                double price = GetPrice(cart);

                AddtoCartVM.TotalPrice += price * cart.Count;
            }

            return View(AddtoCartVM);
        }

        private double GetPrice(ShoppingCart shoppingCart)
        {
            if (shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }
            else
            {
                if (shoppingCart.Count <= 100)
                {
                    return shoppingCart.Product.Price50;
                }
                else
                {
                    return shoppingCart.Product.Price100;
                }
            }

        }

        public IActionResult plus(int cartId)
        {
            var CartID = _unitofwork.ShoppingCart.Get(u=>u.Id==cartId);

            CartID.Count += 1;
            _unitofwork.ShoppingCart.Update(CartID);
            _unitofwork.Save();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult minus(int cartId)
        {
            var CartID = _unitofwork.ShoppingCart.Get(u => u.Id == cartId);

            if (CartID.Count < 1)
            {
                _unitofwork.ShoppingCart.Remove(CartID);
            }
            else
            {
                CartID.Count -= 1;
                _unitofwork.ShoppingCart.Update(CartID);
                _unitofwork.Save();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult remove(int cartId)
        {
            var CartID = _unitofwork.ShoppingCart.Get(u => u.Id == cartId);

            _unitofwork.ShoppingCart.Remove(CartID);

            return RedirectToAction("Index");

        }

    }

    

}






