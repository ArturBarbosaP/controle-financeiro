using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyWeb.Models.Entities;
using MoneyWeb.Models.ViewModels;
using MoneyWeb.Repository.Interfaces;

namespace MoneyWeb.Controllers
{
    public class ContaController : BaseController
    {
        private readonly ICategoriaRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public ContaController(ICategoriaRepository repository, IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        private Task<Usuario> _usuario
        {
            get
            {
                return _usuarioRepository.GetUsuarioById(UsuarioId);
            }
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                Usuario usuario = await _usuario;

                var contas = _mapper.Map<IEnumerable<ContaViewModel>>(usuario.Contas);
                return View(contas);
            }
            catch (Exception ex)
            {
                return ExibirViewErro($"Erro ao listar contas: {ex.Message}");
            }
        }
    }
}