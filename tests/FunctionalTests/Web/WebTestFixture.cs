using Forma1Teams.Infrastructure.Data;
using Forma1Teams.Infrastructure.Identity;
using Forma1Teams.UnitTests.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Web;

namespace Forma1Teams.FunctionalTests.Web
{
    public class WebTestFixture : WebApplicationFactory<Startup>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = base.CreateHost(builder);
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
                    Seed(forma1Context);
                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                    AppIdentityDbContextSeed.SeedAsync(userManager).Wait();

                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            return host;
        }
        private void Seed(Forma1Context forma1Context)
        {
            var teamBuilder = new TeamBuilder();
            forma1Context.Teams.AddRange(teamBuilder.GetList());
            forma1Context.SaveChanges();
        }
        public List<T> GetDbSet<T>() where T : class
        {
            using (var scope = Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var forma1Context = services.GetRequiredService<Forma1Context>();
                return forma1Context.Set<T>().ToList();
            }
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            return base.CreateHostBuilder();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
        }
    }
}
