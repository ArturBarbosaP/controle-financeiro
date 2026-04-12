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
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IContaRepository _contaRepository;
        private readonly ICartaoRepository _cartaoRepository;
        private readonly IMapper _mapper;
        private const string _nomeForm = "LancamentoForm";

        public LancamentoController(ILancamentoRepository repository, IMapper mapper, ICategoriaRepository categoriaRepository, IContaRepository contaRepository, ICartaoRepository cartaoRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
            _contaRepository = contaRepository;
            _cartaoRepository = cartaoRepository;
        }

        private async Task CarregarDadosSelect()
        {
            ViewBag.Categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.GetCategorias(UsuarioId));
            ViewBag.Contas = _mapper.Map<IEnumerable<ContaViewModel>>(await _contaRepository.GetContas(UsuarioId));
            ViewBag.Cartoes = _mapper.Map<IEnumerable<CartaoViewModel>>(await _cartaoRepository.GetCartoes(UsuarioId));
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

        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.Title = "Criar Lançamento";
                ViewBag.Action = "Create";
                await CarregarDadosSelect();

                return View(_nomeForm);
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao criar lançamento: {ex.Message}", false, "Index");
            }
        }
    }
}