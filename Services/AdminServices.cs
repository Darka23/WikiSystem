using Microsoft.AspNetCore.Identity;
using WikiSystem.Contracts;
using WikiSystem.Data.Identity;
using WikiSystem.Data.Repositories;

namespace WikiSystem.Services
{
    public class AdminServices : IAdminServices
    {
        private IApplicationDbRepository repo;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        public AdminServices(IApplicationDbRepository _repo,
            UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager)
        {
            repo = _repo;
            userManager = _userManager;
            roleManager = _roleManager;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return repo.All<ApplicationUser>();
        }

        public void DeleteUser(string id)
        {
            var user = repo.All<ApplicationUser>()
                .Where(x => x.Id == id)
                .First();

            if (user != null)
            {
                repo.Delete<ApplicationUser>(user);
                repo.SaveChanges();
            }
        }

        public async Task<bool> EditUser(ApplicationUser model)
        {
            bool result = false;
            var user = await repo.GetByIdAsync<ApplicationUser>(model.Id);


            if (user != null)
            {
                user.UserName = model.UserName;
                user.PhoneNumber = model.PhoneNumber;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public ApplicationUser? PlaceholderUser(string id)
        {
            return repo.All<ApplicationUser>()
                .Where(r => r.Id == id)
                .Select(r => new ApplicationUser()
                {
                    Id = id,
                    PhoneNumber = r.PhoneNumber,
                    UserName = r.UserName
                })
                .FirstOrDefault();
        }

        public async Task SetUserModerator(string id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            await userManager.RemoveFromRoleAsync(user, "Guest");
            await userManager.AddToRoleAsync(user, "Moderator");

            var repoUser = await repo.GetByIdAsync<ApplicationUser>(id.ToString());
            repoUser.Role = "Moderator";

            await repo.SaveChangesAsync();
        }

        public async Task SetUserDefault(string id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            await userManager.RemoveFromRoleAsync(user,"Moderator");
            await userManager.AddToRoleAsync(user,"Guest");

            var repoUser = await repo.GetByIdAsync<ApplicationUser>(id.ToString());
            repoUser.Role = "Guest";

            await repo.SaveChangesAsync();
        }
    }
}
