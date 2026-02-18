using Microsoft.EntityFrameworkCore;
using MoneyWeb.Data;
using MoneyWeb.Models.Entities;
using MoneyWeb.Repository.Interfaces;

namespace MoneyWeb.Repository
{
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        private readonly ApplicationContext _context;

        public UsuarioRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await _context.Usuarios
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            return await _context.Usuarios
                .ToListAsync();
        }
    }
}