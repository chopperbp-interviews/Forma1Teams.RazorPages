using Forma1Teams.ApplicationCore.Entities;
using Forma1Teams.ApplicationCore.Interfaces;
using Forma1Teams.ApplicationCore.Services;
using Forma1Teams.UnitTests.Builders;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Forma1Teams.UnitTests.Services.BasketServiceTests
{
    public class DeleteTeam
    {
        private readonly Mock<IAsyncRepository<Team>> _mockTeamRepo;
        private readonly TeamBuilder teamBuilder = new TeamBuilder();
        public DeleteTeam()
        {
            _mockTeamRepo = new Mock<IAsyncRepository<Team>>();
        }
        [Fact]
        public async Task ShouldInvokeTeamRepositoryDeleteAsyncOnce()
        {
            var team = teamBuilder.WithDefaultValues();
            _mockTeamRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(team);
            var teamService = new TeamService(_mockTeamRepo.Object);

            await teamService.DeleteTeamAsync(It.IsAny<int>());
            _mockTeamRepo.Verify(x => x.DeleteAsync(It.IsAny<Team>()), Times.Once);
        }
    }
}
