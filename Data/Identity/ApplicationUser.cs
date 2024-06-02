using Microsoft.AspNetCore.Identity;
using WikiSystem.Data.Models;

namespace WikiSystem.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<Article> Articles { get; set; }
        public string? Role { get; set; }
    }
}
