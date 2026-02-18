using System.ComponentModel.DataAnnotations;

namespace MoneyWeb.Models.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome!")]
        [MaxLength(100, ErrorMessage = "O nome não pode ultrapassar 100 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o usuário!")]
        [MaxLength(100, ErrorMessage = "O usuário não pode ultrapassar 100 caracteres!")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "Digite a senha!")]
        [MinLength(5, ErrorMessage = "Sua senha deve conter no mínimo 5 caracteres!")]
        public string Senha { get; set; }

    }
}