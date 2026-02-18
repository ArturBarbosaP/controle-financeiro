namespace MoneyWeb.Models.Entities
{
    public class Categoria : BaseEntity
    {
        public string Nome { get; set; }

        public string Tipo { get; set; }

        public string Cor { get; set; }

        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        public List<Lancamento> Lancamentos { get; set; }
    }
}