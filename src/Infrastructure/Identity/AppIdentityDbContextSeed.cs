using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Forma1Teams.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager)
        {
            var adminUser = new IdentityUser("admin");
            var user = await userManager.CreateAsync(adminUser, "test2020");
            if (user.Succeeded == false)
            {
                throw new Exception("An error occurred seeding AppIdentityDbContext:\r\n" +
                                    string.Join(Environment.NewLine, user.Errors.Select(a => $"Code:{a.Code}, Description: {a.Description}")));
            }
        }
    }
}
