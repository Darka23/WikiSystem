using Microsoft.AspNetCore.Mvc;
using WikiSystem.Data.Models;

namespace WikiSystem.Contracts
{
    public interface ICategoryServices
    {
        Task AddCategory([FromForm] Category category);

        IEnumerable<Category> GetCategories();

        Task<bool> EditCategory([FromForm] Category model);

        void DeleteCategory(int id);
    }
}
