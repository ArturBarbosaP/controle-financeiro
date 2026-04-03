using Microsoft.EntityFrameworkCore;
using MoneyWeb.Data;
using MoneyWeb.Models.Entities;
using MoneyWeb.Repository.Interfaces;

namespace MoneyWeb.Repository
{
    public class CategoriaRepository : BaseRepository, ICategoriaRepository
    {
        private readonly ApplicationContext _context;

        public CategoriaRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Categoria> GetCategoriaById(int id, int usuarioId)
        {
            return await _context.Categorias
                .Where(u => u.UsuarioId == usuarioId)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Categoria>> GetCategorias(int usuarioId)
        {
            return await _context.Categorias
                .Where(u => u.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasDeDespesa(int usuarioId)
        {
            return await _context.Categorias
                .Where(u => u.UsuarioId == usuarioId)
                .Where(c => c.Tipo == "Despesa")
                .ToListAsync();
        }
    }
}
