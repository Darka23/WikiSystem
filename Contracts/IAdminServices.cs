using Microsoft.AspNetCore.Mvc;
using WikiSystem.Data.Identity;
using WikiSystem.Data.Models;

namespace WikiSystem.Contracts
{
    public interface IAdminServices
    {
        IEnumerable<ApplicationUser> GetAllUsers();

        void DeleteUser(string id);

        Task<bool> EditUser(ApplicationUser model);

        ApplicationUser? PlaceholderUser(string id);

        Task SetUserModerator(string id);

        Task SetUserDefault(string id);
    }
}
