using Forma1Teams.Web.Interfaces;
using Forma1Teams.Web.Models.Teams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Forma1Teams.Web.Pages.Teams
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ITeamsModelService teamsModelService;

        public EditModel(ITeamsModelService teamsModelService)
        {
            this.teamsModelService = teamsModelService;
        }
        [BindProperty]
        public Team ViewModel { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            ViewModel = await teamsModelService.GetTeam(id);
            if (ViewModel == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await teamsModelService.UpdateTeam(ViewModel);
            TempData.Add("success", "Sikeres mentés");
            return RedirectToPage("/Teams/Index");
        }
    }
}
