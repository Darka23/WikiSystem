using Microsoft.AspNetCore.Mvc;
using WikiSystem.Contracts;
using WikiSystem.Data.Identity;

namespace WikiSystem.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        private IAdminServices adminServices;
        public AdminController(IAdminServices _adminServices)
        {
            adminServices = _adminServices;
        }
        public IActionResult AdminPanel()
        {
            return View();
        }
        public IActionResult UsersList()
        {
            var users = adminServices.GetAllUsers();

            var model = new
            {
                Users = users,
            };

            return View(model);
        }
        public IActionResult DeleteUser(string id)
        {
            adminServices.DeleteUser(id);

            return RedirectToAction("UsersList", "Admin");
        }
        [HttpGet]
        public IActionResult EditUser(string id)
        {
            var currUser = adminServices.PlaceholderUser(id);
            return View(currUser);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(ApplicationUser model)
        {
            if (await adminServices.EditUser(model))
            {
                return RedirectToAction("UsersList", "Admin");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
        public async Task<IActionResult> SetUserAsModerator(string id)
        {
            await adminServices.SetUserModerator(id);

            return RedirectToAction("UsersList", "Admin");
        }

        public async Task<IActionResult> SetUserAsDefault(string id)
        {
            await adminServices.SetUserDefault(id);

            return RedirectToAction("UsersList", "Admin");
        }
    }
}
