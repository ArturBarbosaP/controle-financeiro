using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyWeb.Models.ViewModels;
using MoneyWeb.Repository.Interfaces;

namespace MoneyWeb.Controllers
{
    public class CartaoController : BaseController
    {
        private readonly ICartaoRepository _repository;
        private readonly IMapper _mapper;
        private const string _nomeForm = "CartaoForm";

        public CartaoController(ICartaoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
    }
}