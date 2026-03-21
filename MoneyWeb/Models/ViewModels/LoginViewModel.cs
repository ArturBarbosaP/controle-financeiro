using System.ComponentModel.DataAnnotations;

namespace MoneyWeb.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Digite o usuário!")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Digite a senha!")]
        public string Senha { get; set; }
    }
}