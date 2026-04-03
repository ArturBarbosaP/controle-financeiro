using MoneyWeb.Models.Entities;

namespace MoneyWeb.Repository.Interfaces
{
    public interface ILancamentoRepository : IBaseRepository
    {
        Task<IEnumerable<Lancamento>> GetLancamentosMensal(int usuarioId, int mes, int ano);

        Task<Lancamento> GetLancamentoById(int id, int usuarioId);
    }
}