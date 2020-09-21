using System.Threading.Tasks;

namespace Forma1Teams.ApplicationCore.Interfaces
{
    public interface ITeamService
    {
        Task CreateTeamAsync(string name, int yearOfFoundation, int wonChampionships, bool paidEntryFee);
        Task UpdateTeamAsync(int teamId, string name, int yearOfFoundation, int wonChampionships, bool paidEntryFee);
        Task DeleteTeamAsync(int teamId);
    }
}
