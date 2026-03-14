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

        public async Task<Categoria> GetCategoriaById(int id)
        {
            return await _context.Categorias
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            return await _context.Categorias
                .ToListAsync();
        }
    }
}
