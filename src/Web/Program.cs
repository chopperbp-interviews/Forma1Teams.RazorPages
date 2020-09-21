using Forma1Teams.Infrastructure.Data;
using Forma1Teams.Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args)
                       .Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var identityDbContext = services.GetRequiredService<AppIdentityDbContext>();
                   // identityDbContext.Database.EnsureCreated();
                    identityDbContext.Database.Migrate();

                    var forma1Context = services.GetRequiredService<Forma1Context>();
                    // forma1Context.Database.EnsureCreated();
                    forma1Context.Database.Migrate();

                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                    await AppIdentityDbContextSeed.SeedAsync(userManager);
                    
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
