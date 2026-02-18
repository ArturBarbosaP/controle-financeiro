namespace MoneyWeb.Models.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; set; }

        public string NomeUsuario { get; set; }

        public string Senha { get; set; }
    }
}