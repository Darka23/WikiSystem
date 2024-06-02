using Microsoft.AspNetCore.Mvc;

namespace WikiSystem.Areas.Moderator.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
