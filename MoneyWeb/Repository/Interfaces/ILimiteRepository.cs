using MoneyWeb.Models.Entities;

namespace MoneyWeb.Repository.Interfaces
{
    public interface ILimiteRepository : IBaseRepository
    {
        Task<IEnumerable<Limite>> GetLimites(int usuarioId);

        Task<Limite> GetLimiteById(int id, int usuarioId);
    }
}