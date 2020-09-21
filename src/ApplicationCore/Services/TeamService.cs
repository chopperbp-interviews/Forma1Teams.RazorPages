using Forma1Teams.ApplicationCore.Entities;
using Forma1Teams.ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace Forma1Teams.ApplicationCore.Services
{
    public class TeamService : ITeamService
    {
        private readonly IAsyncRepository<Team> teamRepository;

        public TeamService(IAsyncRepository<Team> teamRepository)
        {
            this.teamRepository = teamRepository;
        }

        public async Task CreateTeamAsync(string name, int yearOfFoundation, int wonChampionships, bool paidEntryFee)
        {
            var team = new Team(name, yearOfFoundation, wonChampionships, paidEntryFee);
            await teamRepository.AddAsync(team);
        }

        public async Task UpdateTeamAsync(int teamId, string name, int yearOfFoundation, int wonChampionships, bool paidEntryFee)
        {
            var team = await teamRepository.GetByIdAsync(teamId);
            team.UpdateDetails(name, yearOfFoundation, wonChampionships, paidEntryFee);
            await teamRepository.UpdateAsync(team);
        }
        public async Task DeleteTeamAsync(int teamId)
        {
            var team = await teamRepository.GetByIdAsync(teamId);
            await teamRepository.DeleteAsync(team);
        }

    }
}
