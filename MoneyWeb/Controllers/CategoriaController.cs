using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyWeb.Data;
using MoneyWeb.Helpers;
using MoneyWeb.Models.Entities;
using MoneyWeb.Models.ViewModels;
using MoneyWeb.Repository.Interfaces;

namespace MoneyWeb.Controllers
{
    [RequireLogin]
    public class CategoriaController : BaseController
    {
        private readonly ICategoriaRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        private int UsuarioId
        {
            get
            {
                return HttpContext.Session.GetUsuarioLogadoId()!.Value;
            }
        }

        public CategoriaController(ICategoriaRepository repository, IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                Usuario usuario = await _usuarioRepository.GetUsuarioById(UsuarioId);
                var categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(usuario.Categorias);
                return View(categorias);
            }
            catch (Exception ex)
            {
                return ExibirViewErro($"Erro ao listar categorias: {ex.Message}");
            }
        }
    }
}