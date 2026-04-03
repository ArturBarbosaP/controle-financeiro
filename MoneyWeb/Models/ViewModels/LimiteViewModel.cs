using MoneyWeb.Helpers;
using System.ComponentModel.DataAnnotations;

namespace MoneyWeb.Models.ViewModels
{
    public class LimiteViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Valor Limite")]
        [Required(ErrorMessage = "Digite o valor do limite!")]
        [DecimalPrecision(2)]
        public decimal ValorLimite { get; set; }

        [Required(ErrorMessage = "Escolha a categoria do limite!")]
        public int CategoriaId { get; set; }

        public string? CategoriaNome { get; set; }
    }
}