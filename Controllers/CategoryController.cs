using Microsoft.AspNetCore.Mvc;
using WikiSystem.Contracts;
using WikiSystem.Data.Models;

namespace WikiSystem.Controllers
{
    [Area("Moderator")]
    public class CategoryController : BaseController
    {
        private ICategoryServices categoryServices;

        public CategoryController(ICategoryServices _categoryServices)
        {
            categoryServices = _categoryServices;
        }

        public IActionResult ModeratorPanel()
        {
            return View();
        }

        public IActionResult CategoryList()
        {
            var model = categoryServices.GetCategories();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromForm] Category category)
        {
            await categoryServices.AddCategory(category);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory([FromForm] Category category)
        {
            if (await categoryServices.EditCategory(category))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        //public async Task<IActionResult> DeleteCategory(int id)
        //{
        //    await categoryServices.DeleteCategory(id);

        //    return RedirectToAction("Index", "Home");
        //}
    }
}
