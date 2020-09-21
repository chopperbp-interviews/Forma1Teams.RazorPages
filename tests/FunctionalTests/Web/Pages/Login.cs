using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Forma1Teams.FunctionalTests.Web.Pages
{
    [Collection("Sequential")]
    public class Login : IClassFixture<WebTestFixture>
    {
        public HttpClient Client { get; }

        public Login(WebTestFixture factory)
        {
            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        
        [Fact]
        public async Task ReturnsSuccessfulSignInOnPostWithValidCredentials()
        {
            var getResponse = await Client.GetAsync("/account/login");
            getResponse.EnsureSuccessStatusCode();
            string token = await getResponse.GetRequestVerificationToken();

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("UserName", "admin"));
            keyValues.Add(new KeyValuePair<string, string>("Password", "f1test2018"));

            keyValues.Add(new KeyValuePair<string, string>("__RequestVerificationToken", token));
            var formContent = new FormUrlEncodedContent(keyValues);
            
            var postResponse = await Client.PostAsync("/account/login", formContent);
            Assert.Equal(HttpStatusCode.Redirect, postResponse.StatusCode);
            Assert.Equal(new Uri("/", UriKind.Relative), postResponse.Headers.Location);
        }
    }
}
