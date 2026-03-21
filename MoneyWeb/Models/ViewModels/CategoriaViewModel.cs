using MoneyWeb.Helpers;
using System.ComponentModel.DataAnnotations;

namespace MoneyWeb.Models.ViewModels
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome da categoria!")]
        [MaxLength(ErrorMessage = "O nome da categoria não pode ultrapassar 80 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Selecione o tipo da categoria!")]
        [InList(["Despesa", "Receita", "Transf."], ErrorMessage = "O tipo da categoria pode ser apenas Despesa, Receita ou Transf.")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "Selecione a cor da categoria!")]
        [Length(7, 7, ErrorMessage = "Apenas cores em hexadecimal são aceitas!")]
        [RegularExpression("^#[0-9A-Fa-f]{6}$", ErrorMessage = "Apenas cores em hexadecimal são aceitas!")]
        public string Cor { get; set; }

        public int UsuarioId { get; set; }
    }
}