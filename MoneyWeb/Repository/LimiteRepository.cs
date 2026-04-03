using Microsoft.EntityFrameworkCore;
using MoneyWeb.Data;
using MoneyWeb.Models.Entities;
using MoneyWeb.Repository.Interfaces;

namespace MoneyWeb.Repository
{
    public class LimiteRepository : BaseRepository, ILimiteRepository
    {
        private readonly ApplicationContext _context;

        public LimiteRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Limite> GetLimiteById(int id, int usuarioId)
        {
            return await _context.Limites
                .Include(c => c.Categoria)
                .Where(c => c.Categoria.UsuarioId == usuarioId)
                .Where(l => l.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Limite>> GetLimites(int usuarioId)
        {
            return await _context.Limites
                .Include(c => c.Categoria)
                .Where(c => c.Categoria.UsuarioId == usuarioId)
                .ToListAsync();
        }
    }
}