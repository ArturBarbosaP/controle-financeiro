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

        public async Task<Conta> GetContaById(int id)
        {
            return await _context.Contas
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Conta>> GetContas()
        {
            return await _context.Contas
                .ToListAsync();
        }
    }
}