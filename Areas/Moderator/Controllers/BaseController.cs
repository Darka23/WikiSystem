using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WikiSystem.Areas.Moderator.Controllers
{
    [Authorize]
    [Area("Moderator")]
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
