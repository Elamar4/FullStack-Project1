using Bookly.DataAccess.Repository.IRepository;
using Bookly.Models;
using Microsoft.AspNetCore.Mvc;
using Bookly.DataAccess.Data;
using Bookly.DataAccess.Repository;
using Bookly.DataAccess.Repository.IRepository;
using Bookly.Models;
using Microsoft.AspNetCore.Authorization;
using Bookly.Utility;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class CategoryController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        public CategoryController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork; //ujey bunlar back koddlaridi  bunlari ozum yazmisam
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitofWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitofWork.Category.Add(obj);
                _unitofWork.Save();
                TempData["success"] = "Catagory created successfully";

                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category objCategory = _unitofWork.Category.Get(u => u.Id == id);
            return View(objCategory);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitofWork.Category.Update(obj);
                _unitofWork.Save();
                TempData["success"] = "Catagory updated successfully";

                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category objCategory = _unitofWork.Category.Get(u => u.Id == id);
            return View(objCategory);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category obj = _unitofWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {

                return NotFound();
            }
            _unitofWork.Category.Remove(obj);
            _unitofWork.Save();
            TempData["success"] = "Catagory deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
