using Microsoft.EntityFrameworkCore;
using MoneyWeb.Data;
using MoneyWeb.Models.Entities;
using MoneyWeb.Repository.Interfaces;

namespace MoneyWeb.Repository
{
    public class ContaRepository : BaseRepository, IContaRepository
    {
        private readonly ApplicationContext _context;

        public ContaRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Conta> GetContaById(int id, int usuarioId)
        {
            return await _context.Contas
                .Where(u => u.UsuarioId == usuarioId)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Conta>> GetContas(int usuarioId)
        {
            return await _context.Contas
                .Where(u => u.UsuarioId == usuarioId)
                .ToListAsync();
        }
    }
}