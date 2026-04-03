using MoneyWeb.Models.Entities;

namespace MoneyWeb.Repository.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository
    {
        Task<IEnumerable<Usuario>> GetUsuarios();

        Task<Usuario> GetUsuarioById(int id);

        Task<Usuario> GetUsuarioByNomeUsuario(string usuario);
    }
}