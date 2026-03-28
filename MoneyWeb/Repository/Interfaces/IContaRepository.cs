using MoneyWeb.Models.Entities;

namespace MoneyWeb.Repository.Interfaces
{
    public interface IContaRepository : IBaserepository
    {
        Task<IEnumerable<Conta>> GetContas();

        Task<Conta> GetContaById(int id);
    }
}