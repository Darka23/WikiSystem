using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WikiSystem.Contracts;
using WikiSystem.Data.Identity;
using WikiSystem.Data.Models;
using WikiSystem.Data.Repositories;

namespace WikiSystem.Services
{
    public class ArticleServices : IArticleServices
    {
        private IApplicationDbRepository repo;

        public ArticleServices(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddArticle([FromForm] Article article, ApplicationUser user)
        {
            var existing = repo.All<Article>()
               .Where(r => r.Title == article.Title)
               .FirstOrDefault();

            if (existing != null)
            {
                throw new ArgumentException("Article already exist");
            }

            await repo.AddAsync(new Article()
            {
                Title = article.Title,
                CategoryId = article.CategoryId,
                Description = article.Description,
                IsLocked = false,
                LastEditDate = DateTime.Now,
                PublishDate = DateTime.Now,
                PublisherId = user.Id,
                Publisher = user,
            });

            await repo.SaveChangesAsync();
        }

        public async Task<Article?> ArticleForPlaceholder(int id)
        {
            return await repo.GetByIdAsync<Article>(id);
        }

        public void DeleteArticle(int id)
        {
            var article = repo.All<Article>().Where(x=>x.Id==id).First();
            repo.Delete(article);
            repo.SaveChanges();
        }

        public async Task<bool> EditArticle([FromForm] Article model)
        {
            bool result = false;
            var article = await repo.GetByIdAsync<Article>(model.Id);

            if (article != null)
            {
                article.Title = model.Title;
                article.Description = model.Description;
                article.IsLocked = model.IsLocked ? model.IsLocked : false;
                article.CategoryId = model.CategoryId;
                article.LastEditDate = DateTime.Now;
                article.PublishDate = article.PublishDate;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public IEnumerable<Article> GetArticles()
        {
            return repo.All<Article>()
                .Include(x=>x.Category)
                .Include(x=>x.Publisher)
                .ToList();
        }

        public Article GetArticleById(int id)
        {
            return repo.All<Article>()
                .Include(x => x.Category)
                .Include(x => x.Publisher)
                .Where(x=>x.Id == id)
                .First();
        }
    }
}
