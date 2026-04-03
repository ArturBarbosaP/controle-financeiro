using MoneyWeb.Helpers;
using System.ComponentModel.DataAnnotations;

namespace MoneyWeb.Models.ViewModels
{
    public class CartaoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do cartão!")]
        [MaxLength(80, ErrorMessage = "O nome do cartão não pode ultrapassar 80 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Escolha a data de fechamento do cartão!")]
        public DateOnly DataFechamento { get; set; }

        [Required(ErrorMessage = "Escolha a data de vencimento do cartão!")]
        public DateOnly DataVencimento { get; set; }

        [Display(Name = "Limite")]
        [Required(ErrorMessage = "Digite o limite do cartão!")]
        [DecimalPrecision(2)]
        public decimal Limite { get; set; }

        [Display(Name = "Limite Disponivel")]
        [Required(ErrorMessage = "Digite o limite disponível do cartão!")]
        [DecimalPrecision(2)]
        public decimal LimiteDisponivel { get; set; }

        [Display(Name = "Valor Parcelado")]
        [Required(ErrorMessage = "Digite o valor parcelado do cartão!")]
        [DecimalPrecision(2)]
        public decimal ValorParcelado { get; set; }
    }
}