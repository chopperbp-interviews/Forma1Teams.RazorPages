using Forma1Teams.ApplicationCore.Entities;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Forma1Teams.FunctionalTests.Web.Pages
{
    [Collection("Sequential")]
    public class TeamDeletePage : IClassFixture<WebTestFixture>
    {
        private readonly WebTestFixture factory;

        public TeamDeletePage(WebTestFixture factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task Post_Delete_ReturnsRedirectToRoot()
        {
            var client = factory.CreateClientWithTestAuth();
            var id = factory.GetDbSet<Team>().First().Id.ToString();

            var response = await client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            string token = await response.GetRequestVerificationToken();

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("__RequestVerificationToken", token));
            var formContent = new FormUrlEncodedContent(keyValues);
            var query = new Dictionary<string, string> { { "id", id } };
            var postResponse = await client.PostAsync(QueryHelpers.AddQueryString("/Teams/Delete", query), formContent);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, postResponse.StatusCode);
            Assert.Equal("/", postResponse.Headers.Location.OriginalString);
        }
    }
}
