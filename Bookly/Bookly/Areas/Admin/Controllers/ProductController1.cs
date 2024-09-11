using Bookly.DataAccess.Repository.IRepository;
using Bookly.Models.ViewModels;
using Bookly.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bookly.DataAccess.Data;
using Bookly.DataAccess.Repository;
using Bookly.Utility;
using Microsoft.AspNetCore.Authorization;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class ProductController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofWork = unitofWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitofWork.Product.GetAll(inculudeProperties:"Category").ToList();
            return View(objProductList);
        }

        /// /////////////////////////////////////////////////////

        public IActionResult Upsert(int? id)
        {

            ProductVM productVM = new()
            {
                CategoryList = _unitofWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                Product = new Product()
            };


            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitofWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filenName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageURL))
                    {
                        //delete old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageURL.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, filenName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageURL = @"\images\product\" + filenName;
                }
                if (productVM.Product.Id == 0)
                {
                    _unitofWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitofWork.Product.Update(productVM.Product);
                }

                _unitofWork.Save();
                TempData["success"] = "Catagory created successfully";

                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitofWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
            }
            return View(productVM);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product objProduct = _unitofWork.Product.Get(u => u.Id == id);
            return View(objProduct);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            Product obj = _unitofWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {

                return NotFound();
            }
            _unitofWork.Product.Remove(obj);
            _unitofWork.Save();
            TempData["success"] = "Catagory deleted successfully";
            return RedirectToAction("Index");

        }

        #region API CALSS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitofWork.Product.GetAll(inculudeProperties: "Category").ToList();
            return Json(new { data = objProductList });

        }


        #endregion
    }
}
