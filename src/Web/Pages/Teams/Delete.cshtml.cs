using Forma1Teams.ApplicationCore.Interfaces;
using Forma1Teams.Web.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Forma1Teams.Web.Pages.Teams
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ITeamsModelService teamsModelService;
        private readonly ITeamService teamService;

        public DeleteModel(ITeamsModelService teamsModelService, ITeamService teamService)
        {
            this.teamsModelService = teamsModelService;
            this.teamService = teamService;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var team = await teamsModelService.GetTeam(id);
            if (team == null)
            {
                return NotFound();
            }
            await teamService.DeleteTeamAsync(id);
            TempData.Add("success", "Sikeres törlés");
            return RedirectToPage("./Index");
        }
    }
}
