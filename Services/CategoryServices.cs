using Microsoft.AspNetCore.Mvc;
using WikiSystem.Contracts;
using WikiSystem.Data.Models;
using WikiSystem.Data.Repositories;

namespace WikiSystem.Services
{
    public class CategoryServices : ICategoryServices
    {
        private IApplicationDbRepository repo;

        public CategoryServices(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }
        public async Task AddCategory([FromForm] Category category)
        {
            var existing = repo.All<Category>()
               .Where(r => r.Name == category.Name)
               .FirstOrDefault();

            if (existing != null)
            {
                throw new ArgumentException("Category already exist");
            }

            await repo.AddAsync(new Category()
            {
                Name = category.Name,
            });

            await repo.SaveChangesAsync();
        }

        public async Task<bool> EditCategory([FromForm] Category model)
        {
            bool result = false;
            var category = await repo.GetByIdAsync<Category>(model.Id);

            if (category != null)
            {
                category.Name = model.Name;

                await repo.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public void DeleteCategory(int id)
        {
            var category = repo.All<Category>().Where(r => r.Id == id).First();
            repo.Delete(category);
            repo.SaveChanges();
        }
        public IEnumerable<Category> GetCategories()
        {
            return repo.All<Category>();
        }
    }
}
