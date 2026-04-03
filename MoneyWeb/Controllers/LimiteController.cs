using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyWeb.Helpers;
using MoneyWeb.Models.ViewModels;
using MoneyWeb.Repository.Interfaces;

namespace MoneyWeb.Controllers
{
    [RequireLogin]
    public class LimiteController : BaseController
    {
        private readonly ILimiteRepository _repository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;
        private const string _nomeForm = "LimiteForm";

        public LimiteController(ILimiteRepository repository, ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _repository = repository;
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var limites = _mapper.Map<IEnumerable<LimiteViewModel>>(await _repository.GetLimites(UsuarioId));
                return View(limites);
            }
            catch (Exception ex)
            {
                return ExibirViewErro(ex.Message);
            }
        }
    }
}