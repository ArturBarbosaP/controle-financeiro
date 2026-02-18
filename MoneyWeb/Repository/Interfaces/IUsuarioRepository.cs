using MoneyWeb.Models.Entities;

namespace MoneyWeb.Repository.Interfaces
{
    public interface IUsuarioRepository : IBaserepository
    {
        Task<IEnumerable<Usuario>> GetUsuarios();

        Task<Usuario> GetUsuarioById(int id);
    }
}