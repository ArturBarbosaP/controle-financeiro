using Microsoft.EntityFrameworkCore;
using MoneyWeb.Data;
using MoneyWeb.Models.Entities;
using MoneyWeb.Repository.Interfaces;

namespace MoneyWeb.Repository
{
    public class LancamentoRepository : BaseRepository, ILancamentoRepository
    {
        private readonly ApplicationContext _context;

        public LancamentoRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Lancamento> GetLancamentoById(int id, int usuarioId)
        {
            return await _context.Lancamentos
                .Where(x => x.UsuarioId == usuarioId)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Lancamento>> GetLancamentosMensal(int usuarioId, int mes, int ano)
        {
            DateOnly dataInicio = new(ano, mes, 1);
            DateOnly dataFim = dataInicio.AddMonths(1).AddDays(-1);

            return await _context.Lancamentos
                .Where(x => x.UsuarioId == usuarioId)
                .Where(x => x.Data >= dataInicio && x.Data <= dataFim)
                .ToListAsync();
        }
    }
}