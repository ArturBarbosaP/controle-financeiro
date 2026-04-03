using MoneyWeb.Models.Entities;

namespace MoneyWeb.Repository.Interfaces
{
    public interface ICartaoRepository : IBaseRepository
    {
        Task<IEnumerable<Cartao>> GetCartoes(int usuarioId);

        Task<Cartao> GetCartaoById(int id, int usuarioId);
    }
}