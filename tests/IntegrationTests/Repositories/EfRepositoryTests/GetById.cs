using Forma1Teams.ApplicationCore.Entities;
using Forma1Teams.Infrastructure.Data;
using Forma1Teams.UnitTests.Builders;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Forma1Teams.IntegrationTests.Repositories.EfRepositoryTests
{
    public class GetById
    {
        private readonly ITestOutputHelper output;
        private Forma1Context forma1Context;
        private EfRepository<Team> efRepository;
        private TeamBuilder TeamBuilder { get; } = new TeamBuilder();

        public GetById(ITestOutputHelper output)
        {
            var dbOptions = new DbContextOptionsBuilder<Forma1Context>()
                .UseInMemoryDatabase(databaseName: "TestCatalog")
                .Options;
            forma1Context = new Forma1Context(dbOptions);
            efRepository = new EfRepository<Team>(forma1Context);
            this.output = output;
        }

        [Fact]
        public async Task GetsExistingOrder()
        {
            var existingTeams = TeamBuilder.GetList();
            forma1Context.Teams.AddRange(existingTeams);
            forma1Context.SaveChanges();
            var team = existingTeams.First();
            output.WriteLine($"teamId: {team.Id}");

            var teamFromRepo = await efRepository.GetByIdAsync(team.Id);

            Assert.Equal(team.Id, teamFromRepo.Id);
            Assert.Equal(team.Name, teamFromRepo.Name);
            Assert.Equal(team.PaidEntryFee, teamFromRepo.PaidEntryFee);
            Assert.Equal(team.WonChampionships, teamFromRepo.WonChampionships);
            Assert.Equal(team.YearOfFoundation, teamFromRepo.YearOfFoundation);
        }
    }
}
