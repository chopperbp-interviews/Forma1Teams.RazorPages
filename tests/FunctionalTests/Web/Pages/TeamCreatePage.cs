using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Forma1Teams.FunctionalTests.Web.Pages
{
    [Collection("Sequential")]
    public class TeamCreatePage : IClassFixture<WebTestFixture>
    {
        private readonly WebTestFixture factory;

        public TeamCreatePage(WebTestFixture factory)
        {
            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            this.factory = factory;
        }
        public HttpClient Client { get; }
        public object HtmlHelpers { get; private set; }

        [Fact]
        public async Task Post_ReturnsRedirectToRoot()
        {
            var client = factory.CreateClientWithTestAuth();

            var response = await client.GetAsync("/Teams/Create");
            response.EnsureSuccessStatusCode();
            string token = await response.GetRequestVerificationToken();

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("Name", "Test TeamXXX"));
            keyValues.Add(new KeyValuePair<string, string>("YearOfFoundation", "2000"));
            keyValues.Add(new KeyValuePair<string, string>("WonChampionships", "19"));
            keyValues.Add(new KeyValuePair<string, string>("PaidEntryFee", "true"));
            keyValues.Add(new KeyValuePair<string, string>("__RequestVerificationToken", token));
            var formContent = new FormUrlEncodedContent(keyValues);

            var postResponse = await client.PostAsync("/Teams/Create", formContent);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, postResponse.StatusCode);
            Assert.Equal("/", postResponse.Headers.Location.OriginalString);

            var resultresponse = await client.GetAsync("/");
            resultresponse.EnsureSuccessStatusCode();
            var stringResponse = await resultresponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains("Test TeamXXX", stringResponse);
        }
    }
}
