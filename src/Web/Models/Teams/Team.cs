using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Forma1Teams.Web.Models.Teams
{
    public class Team
    {
        [HiddenInput]
        public int Id { get; set; }
        [Display(Name = "Név", Prompt ="Név")]
        [Required(ErrorMessage = "Név megadása kötelező.")]
        public string Name { get; set; }
        [Display(Name = "Alapítás éve", Prompt = "Alapítás éve")]
        [Required(ErrorMessage = "Alapítás éve megadása kötelező.")]
        public int YearOfFoundation { get; set; }
        [Display(Name = "Megnyert világbajnokságok száma", Prompt = "Megnyert világbajnokságok száma")]
        [Required(ErrorMessage = "Megnyert világbajnokságok száma  megadása kötelező.")]
        public int WonChampionships { get; set; }
        [Display(Name = "Nevezési díjat befizette")]
        [Required(ErrorMessage = "Nevezési díjat befizette megadása kötelező.")]
        public bool PaidEntryFee { get; set; }
    }
}
