using MoneyWeb.Models.Entities;

namespace MoneyWeb.Repository.Interfaces
{
    public interface ICategoriaRepository : IBaserepository
    {
        Task<IEnumerable<Categoria>> GetCategorias();

        Task<Categoria> GetCategoriaById(int id);
    }
}