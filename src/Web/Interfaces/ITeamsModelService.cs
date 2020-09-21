using Forma1Teams.Web.Models.Teams;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forma1Teams.Web.Interfaces
{
    public interface ITeamsModelService
    {
        Task<Team> GetTeam(int teamId);
        IAsyncEnumerable<Team> GetTeams();
        Task UpdateTeam(Team team);
        Task CreateateTeam(Team team);
    }
}
