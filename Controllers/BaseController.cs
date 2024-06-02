using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WikiSystem.Controllers
{
    
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
