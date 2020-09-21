using Forma1Teams.UnitTests.Builders;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Forma1Teams.FunctionalTests.Web.Pages
{
    [Collection("Sequential")]
    public class TeamListOnGet : IClassFixture<WebTestFixture>
    {
        private readonly TeamBuilder teamBuilder = new TeamBuilder();
        public TeamListOnGet(WebTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }
        [Fact]
        public async Task ReturnsHomePageWithTeamListing()
        {
            // Arrange & Act
            var response = await Client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains(teamBuilder.TestName, stringResponse);
        }
    }
}
