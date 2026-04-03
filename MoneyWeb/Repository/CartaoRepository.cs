using Microsoft.EntityFrameworkCore;
using MoneyWeb.Data;
using MoneyWeb.Models.Entities;
using MoneyWeb.Repository.Interfaces;

namespace MoneyWeb.Repository
{
    public class CartaoRepository : BaseRepository, ICartaoRepository
    {
        private readonly ApplicationContext _context;

        public CartaoRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Cartao> GetCartaoById(int id, int usuarioId)
        {
            return await _context.Cartoes
                .Include(c => c.Conta)
                .Where(co => co.Conta.UsuarioId == usuarioId)
                .Where(ca => ca.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Cartao>> GetCartoes(int usuarioId)
        {
            return await _context.Cartoes
                .Include(c => c.Conta)
                .Where(co => co.Conta.UsuarioId == usuarioId)
                .ToListAsync();
        }
    }
}