using Forma1Teams.ApplicationCore.Entities;
using Forma1Teams.UnitTests.Builders;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Forma1Teams.FunctionalTests.Web.Pages
{
    [Collection("Sequential")]
    public class TeamEditPage : IClassFixture<WebTestFixture>
    {
        private readonly WebTestFixture factory;
        private readonly TeamBuilder teamBuilder = new TeamBuilder();

        public TeamEditPage(WebTestFixture factory)
        {
            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            this.factory = factory;
        }
        public HttpClient Client { get; }

        [Fact]
        public async Task Post_ReturnsRedirectToRoot()
        {
            var client = factory.CreateClientWithTestAuth();
            var id = factory.GetDbSet<Team>().First().Id.ToString();

            var response = await client.GetAsync($"/Teams/Edit?id={id}");
            response.EnsureSuccessStatusCode();
            string token = await response.GetRequestVerificationToken();

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("Id", id));
            keyValues.Add(new KeyValuePair<string, string>("Name", "Test TeamNew"));
            keyValues.Add(new KeyValuePair<string, string>("YearOfFoundation", "2000"));
            keyValues.Add(new KeyValuePair<string, string>("WonChampionships", "19"));
            keyValues.Add(new KeyValuePair<string, string>("PaidEntryFee", "true"));
            keyValues.Add(new KeyValuePair<string, string>("__RequestVerificationToken", token));
            var formContent = new FormUrlEncodedContent(keyValues);

            var postResponse = await client.PostAsync("/Teams/Edit", formContent);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, postResponse.StatusCode);
            Assert.Equal("/", postResponse.Headers.Location.OriginalString);

            var resultresponse = await client.GetAsync("/");
            resultresponse.EnsureSuccessStatusCode();
            var stringResponse = await resultresponse.Content.ReadAsStringAsync();

            Assert.Contains("Test TeamNew", stringResponse);
        }
    }
}
