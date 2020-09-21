using Forma1Teams.ApplicationCore.Interfaces;
using Forma1Teams.ApplicationCore.Services;
using Forma1Teams.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forma1Teams.Web.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<ITeamService, TeamService>();
            return services;
        }
    }
}

