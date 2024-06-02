using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WikiSystem.Contracts;
using WikiSystem.Data.Identity;
using WikiSystem.Data.Models;

namespace WikiSystem.Controllers
{
    public class ArticleController : BaseController
    {
        private IArticleServices articleServices;
        private ICategoryServices categoryServices;
        private readonly UserManager<ApplicationUser> userManager;

        public ArticleController(IArticleServices _articleServices, ICategoryServices _categoryServices, UserManager<ApplicationUser> _userManager)
        {
            articleServices = _articleServices;
            categoryServices = _categoryServices;
            userManager = _userManager;
        }

        public IActionResult ListArticle(string articleTitle, string articleDesc, string category)
        {
            var model = articleServices.GetArticles().ToList();

            if (!string.IsNullOrEmpty(articleTitle))
            {
                model = model.Where(x => x.Title.ToLower().Contains(articleTitle.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(articleDesc))
            {
                model = model.Where(x => x.Description.ToLower().Contains(articleDesc.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(category))
            {
                model = model.Where(x => x.Category.Name == category).ToList();
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddArticle()
        {
            ViewData["CategoryId"] = new SelectList(categoryServices.GetCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle([FromForm] Article article)
        {
            var currUser = await userManager.GetUserAsync(HttpContext.User);

            await articleServices.AddArticle(article,currUser);

            return RedirectToAction("ListArticle", "Article");
        }

        [HttpGet]
        public async Task<IActionResult> EditArticle(int id)
        {
            var placeholder = await articleServices.ArticleForPlaceholder(id);
            ViewData["CategoryId"] = new SelectList(categoryServices.GetCategories(), "Id", "Name");
            return View(placeholder);
        }

        [HttpPost]
        public async Task<IActionResult> EditArticle([FromForm] Article article)
        {
            if (await articleServices.EditArticle(article))
            {
                return RedirectToAction("ListArticle", "Article");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult DeleteArticle(int id)
        {
            articleServices.DeleteArticle(id);

            return RedirectToAction("ListArticle", "Article");
        }

        public IActionResult DetailsArticle(int id)
        {
            var article = articleServices.GetArticleById(id);
            return View(article);
        }
    }
}
