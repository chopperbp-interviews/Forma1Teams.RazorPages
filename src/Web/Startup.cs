using Forma1Teams.Infrastructure.Data;
using Forma1Teams.Infrastructure.Identity;
using Forma1Teams.Web.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Web
{
    public class Startup
    {
        private SqliteConnection inMemorySqlite;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration configuration;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            this.env = env;
            this.configuration = configuration;
        }

        private void ConfigureInMemoryDatabases(IServiceCollection services)
        {
            inMemorySqlite = new SqliteConnection("Data Source=:memory:");
            inMemorySqlite.Open();

            services.AddDbContext<Forma1Context>(c =>
                {
                    c.UseSqlite(inMemorySqlite);
                    c.EnableSensitiveDataLogging();
                });
            services.AddDbContext<AppIdentityDbContext>(c =>
                {
                    c.UseSqlite(inMemorySqlite);
                    c.EnableSensitiveDataLogging();
                });

        }

        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-3.1
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureInMemoryDatabases(services);
            ConfigureServices(services);
        }
        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureInMemoryDatabases(services);
            ConfigureServices(services);

        }
        public void ConfigureTestingServices(IServiceCollection services)
        {
            ConfigureInMemoryDatabases(services);
            ConfigureServices(services);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services
              .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.Cookie.HttpOnly = true;
                  options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                  options.Cookie.SameSite = SameSiteMode.Lax;
              });
            services.AddAuthorization();
            services
                .AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;
                })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddCoreServices(configuration);
            services.AddWebServices(configuration);

            var builder = services
                .AddRazorPages(options =>
                {
                    options.Conventions.AddPageRoute("/Teams/Index", "");
                });
#if DEBUG
            if (env.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }
#endif
        }

        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Errors/500");
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
