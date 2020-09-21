using Forma1Teams.ApplicationCore.Interfaces;
using Forma1Teams.Web.Interfaces;
using Forma1Teams.Web.Models.Teams;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forma1Teams.Web.Services
{
    public class TeamsModelService : ITeamsModelService
    {
        private readonly IAsyncRepository<ApplicationCore.Entities.Team> teamRepository;
        private readonly ITeamService teamService;

        public TeamsModelService(IAsyncRepository<ApplicationCore.Entities.Team> teamRepository, 
                                 ITeamService teamService)
        {
            this.teamRepository = teamRepository;
            this.teamService = teamService;
        }
        public IAsyncEnumerable<Team> GetTeams()
        {
            return teamRepository
                .GetQuery()
                .Select(c => new Team
                {
                    Id = c.Id,
                    Name = c.Name,
                    PaidEntryFee = c.PaidEntryFee,
                    WonChampionships = c.WonChampionships,
                    YearOfFoundation = c.YearOfFoundation
                })
                .OrderByDescending(c => c.Name)
                .AsAsyncEnumerable();
        }

        public async Task UpdateTeam(Team team)
        {
            await teamService.UpdateTeamAsync(team.Id, team.Name, team.YearOfFoundation, team.WonChampionships, team.PaidEntryFee);
        }
        public async Task CreateateTeam(Team team)
        {
            await teamService.CreateTeamAsync(team.Name, team.YearOfFoundation, team.WonChampionships, team.PaidEntryFee);
        }

        public async Task<Team> GetTeam(int teamId)
        {
            return await teamRepository
              .GetQuery()
              .Where(x => x.Id == teamId)
              .Select(c => new Team
              {
                  Id = c.Id,
                  Name = c.Name,
                  PaidEntryFee = c.PaidEntryFee,
                  WonChampionships = c.WonChampionships,
                  YearOfFoundation = c.YearOfFoundation
              })
              .SingleOrDefaultAsync();
        }
    }
}
