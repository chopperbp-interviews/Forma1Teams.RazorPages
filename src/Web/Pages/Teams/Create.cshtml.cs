using Forma1Teams.Web.Interfaces;
using Forma1Teams.Web.Models.Teams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Forma1Teams.Web.Pages.Teams
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ITeamsModelService teamsModelService;

        public CreateModel(ITeamsModelService teamsModelService)
        {
            this.teamsModelService = teamsModelService;
        }
        [BindProperty]
        public Team ViewModel { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await teamsModelService.CreateateTeam(ViewModel);
            TempData.Add("success", "Sikeres létrehozás");
            return RedirectToPage("/Teams/Index");
        }
    }
}
