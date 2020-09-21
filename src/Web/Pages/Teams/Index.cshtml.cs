using Forma1Teams.Web.Interfaces;
using Forma1Teams.Web.Models.Teams;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forma1Teams.Web.Pages.Teams
{
    public class IndexModel : PageModel
    {
        private readonly ITeamsModelService teamsModelService;

        public IndexModel(ITeamsModelService teamsModelService)
        {
            this.teamsModelService = teamsModelService;
        }
        public List<Team> ViewModel { get; set; } = new List<Team>();
        
        public async Task OnGetAsync()
        {
            ViewModel = await teamsModelService.GetTeams().ToListAsync();
        }
    }
}
