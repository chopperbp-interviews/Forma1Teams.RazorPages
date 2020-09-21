using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Forma1Teams.FunctionalTests.Web.Pages
{
    [Collection("Sequential")]
    public class BasicTests : IClassFixture<WebTestFixture>
    {
        private readonly WebTestFixture factory;

        public BasicTests(WebTestFixture factory)
        {
            this.factory = factory;
        }

        [Theory]
        [InlineData("/")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Teams/Create")]
        [InlineData("/Teams/Edit")]
        [InlineData("/Teams/Delete")]
        public async Task Get_RedirectsToLoginIfNotAuthenticated(string url)
        {
            var client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Contains("/Account/Login", response.Headers.Location.OriginalString);
        }

        [Theory]
        [InlineData("/Teams/Create")]
        [InlineData("/Teams/Edit?id=1")]
        [InlineData("/Teams/Delete?id=1")]
        public async Task Get_SecurePageIsReturnedForAnAuthenticatedUser(string url)
        {
            // Arrange
            var client = factory.CreateClientWithTestAuth();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Teams/Edit")]
        public async Task Get_NotFoundIsReturnedForAnAuthenticatedUser(string url)
        {
            // Arrange
            var client = factory.CreateClientWithTestAuth();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
