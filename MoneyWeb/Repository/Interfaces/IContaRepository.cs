using MoneyWeb.Models.Entities;

namespace MoneyWeb.Repository.Interfaces
{
    public interface IContaRepository : IBaseRepository
    {
        Task<IEnumerable<Conta>> GetContas(int usuarioId);

        Task<Conta> GetContaById(int id, int usuarioId);
    }
}