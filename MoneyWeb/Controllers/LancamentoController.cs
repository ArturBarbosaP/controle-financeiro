using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyWeb.Helpers;
using MoneyWeb.Models.ViewModels;
using MoneyWeb.Repository.Interfaces;

namespace MoneyWeb.Controllers
{
    [RequireLogin]
    public class LancamentoController : BaseController
    {
        private readonly ILancamentoRepository _repository;
        private readonly IMapper _mapper;
        private const string _nomeForm = "LancamentoForm";

        public LancamentoController(ILancamentoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var lancamentos = _mapper.Map<IEnumerable<LancamentoViewModel>>(await _repository.GetLancamentosMensal(UsuarioId, DateTime.Now.Month, DateTime.Now.Year));
                return View(lancamentos);
            }
            catch (Exception ex)
            {
                return ExibirViewErro(ex.Message);
            }
        }
    }
}