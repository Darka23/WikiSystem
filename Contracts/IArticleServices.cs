using Microsoft.AspNetCore.Mvc;
using WikiSystem.Data.Identity;
using WikiSystem.Data.Models;

namespace WikiSystem.Contracts
{
    public interface IArticleServices
    {
        Task AddArticle([FromForm] Article article, ApplicationUser user);
        IEnumerable<Article> GetArticles();
        Task<bool> EditArticle([FromForm] Article article);
        void DeleteArticle(int id);
        Task<Article?> ArticleForPlaceholder(int id);
        Article GetArticleById(int id);
    }
}
