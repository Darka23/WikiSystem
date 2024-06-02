using Microsoft.AspNetCore.Mvc;
using WikiSystem.Contracts;
using WikiSystem.Data.Models;

namespace WikiSystem.Areas.Moderator.Controllers
{
    public class ModeratorController : BaseController
    {
        private ICategoryServices categoryServices;

        public ModeratorController(ICategoryServices _categoryServices)
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

            return RedirectToAction("ModeratorPanel", "Moderator", new {area="Moderator"});
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
                return RedirectToAction("ModeratorPanel", "Moderator", new { area = "Moderator" });
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult DeleteCategory(int id)
        {
            categoryServices.DeleteCategory(id);

            return RedirectToAction("ModeratorPanel", "Moderator", new { area = "Moderator" });
        }
    }
}

