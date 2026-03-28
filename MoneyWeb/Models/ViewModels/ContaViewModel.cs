using MoneyWeb.Helpers;
using System.ComponentModel.DataAnnotations;

namespace MoneyWeb.Models.ViewModels
{
    public class ContaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome da conta!")]
        [MaxLength(ErrorMessage = "O nome da conta não pode ultrapassar 80 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o saldo da conta!")]
        [DecimalPrecision(2)]
        public decimal Saldo { get; set; }
    }
}