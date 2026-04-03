using MoneyWeb.Models.Entities;

namespace MoneyWeb.Repository.Interfaces
{
    public interface ICategoriaRepository : IBaseRepository
    {
        Task<IEnumerable<Categoria>> GetCategorias(int usuarioId);

        Task<Categoria> GetCategoriaById(int id, int usuarioId);
    }
}