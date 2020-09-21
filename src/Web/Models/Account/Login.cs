using System.ComponentModel.DataAnnotations;

namespace Forma1Teams.Web.Models.Account
{
    public class Login
    {
        [Display(Name = "Felhasználónév")]
        [Required(ErrorMessage = "Felhasználónév megadása kötelező.")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Jelszó", Prompt = "Jelszó")]
        [Required(ErrorMessage = "Jelszó megadása kötelező.")] 
        public string Password { get; set; }

        [Display(Name = "Emlékezz rám?")]
        public bool RememberMe { get; set; }
    }
}
