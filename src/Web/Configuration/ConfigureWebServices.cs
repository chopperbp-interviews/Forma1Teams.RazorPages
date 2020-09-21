using Forma1Teams.Web.Interfaces;
using Forma1Teams.Web.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forma1Teams.Web.Configuration
{
    public static class ConfigureWebServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITeamsModelService, TeamsModelService>();
            return services;
        }
    }
}
