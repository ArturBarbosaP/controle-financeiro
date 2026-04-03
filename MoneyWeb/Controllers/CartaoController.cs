using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyWeb.Helpers;
using MoneyWeb.Models.Entities;
using MoneyWeb.Models.ViewModels;
using MoneyWeb.Repository.Interfaces;

namespace MoneyWeb.Controllers
{
    [RequireLogin]
    public class CartaoController : BaseController
    {
        private readonly ICartaoRepository _repository;
        private readonly IContaRepository _contaRepository;
        private readonly IMapper _mapper;
        private const string _nomeForm = "CartaoForm";

        public CartaoController(ICartaoRepository repository, IContaRepository contaRepository, IMapper mapper)
        {
            _repository = repository;
            _contaRepository = contaRepository;
            _mapper = mapper;
        }

        private async Task<IEnumerable<Conta>> GetContas()
        {
            return await _contaRepository.GetContas(UsuarioId);
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var cartoes = _mapper.Map<IEnumerable<CartaoViewModel>>(await _repository.GetCartoes(UsuarioId));
                return View(cartoes);
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
                ViewBag.Title = "Criar Cartão";
                ViewBag.Action = "Create";
                ViewBag.Contas = await GetContas();

                return View(_nomeForm);
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao criar cartão: {ex.Message}", false, "Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CartaoViewModel cartaoViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = "Criar Cartão";
                    ViewBag.Action = "Create";
                    ViewBag.Contas = await GetContas();

                    return View(_nomeForm, cartaoViewModel);
                }

                Cartao cartaoInsert = _mapper.Map<Cartao>(cartaoViewModel);

                _repository.Add(cartaoInsert);

                if (!await _repository.SaveChanges())
                    throw new Exception("Não foi possível criar no banco de dados!");

                return ExibirMensagem("Cartão criado com sucesso!", true, "Index");
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao criar cartão: {ex.Message}", false, "Index");
            }
        }
    }
}