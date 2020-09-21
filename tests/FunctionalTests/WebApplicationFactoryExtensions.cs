using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Forma1Teams.FunctionalTests
{
    public static class WebApplicationFactoryExtensions
    {
        public static WebApplicationFactory<T> WithFakePolicyEvaluator<T>(this WebApplicationFactory<T> factory) where T : class
        {
            return factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                });
            });
        }

        public static HttpClient CreateClientWithTestAuth<T>(this WebApplicationFactory<T> factory) where T : class
        {
            var client = factory.WithFakePolicyEvaluator().CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            return client;
        }
    }
}
