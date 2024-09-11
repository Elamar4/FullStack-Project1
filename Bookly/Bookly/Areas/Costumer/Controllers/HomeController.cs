using Bookly.DataAccess.Repository.IRepository;
using Bookly.Models;
using Bookly.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.Collections;
using System.Diagnostics;
using System.Drawing.Text;
using System.Security.Claims;

namespace Bookly.Areas.Costumer.Controllers
{
    [Area("Costumer")]
    

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork _unitofWork;

        public HomeController(ILogger<HomeController> logger,IUnitofWork unitofWork)
        {
            _logger = logger;
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitofWork.Product.GetAll(inculudeProperties:"Category");
            return View(productList);
        }
        [Authorize]
        public ActionResult Details(int productId)
        {
            ShoppingCart cart = new()
            {
                Product = _unitofWork.Product.Get(u => u.Id == productId, inculudeProperties: "Category"),
                Count = 1,
                ProductId= productId

            };

            return View(cart);
        }
         
        [HttpPost]
        public ActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userid = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userid;

            ShoppingCart count = _unitofWork.ShoppingCart.Get(u=>u.ApplicationUserId == userid);

            if (shoppingCart.Product !=null)
            {
                count.Count = shoppingCart.Count;

            }

            else
            {
                _unitofWork.ShoppingCart.Add(shoppingCart);
            }           
            _unitofWork.Save();

            return RedirectToAction("Index");
        }


        
       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}