using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyWeb.Models.Entities;
using MoneyWeb.Models.ViewModels;
using MoneyWeb.Repository.Interfaces;
using System.Threading.Tasks;

namespace MoneyWeb.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("UsuarioForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioViewModel usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("UsuarioForm", usuario);

                Usuario usuarioInsert = _mapper.Map<Usuario>(usuario);
                _repository.Add(usuarioInsert);

                if (!await _repository.SaveChanges())
                    return ExibirMensagem("Não foi possível salvar o usuário no banco de dados!", false, "Index", "Usuario");

                return ExibirMensagem("Usuário criado com sucesso!", true, "Index", "Usuario");
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao criar usuário: {ex.Message}", false, "Index", "Usuario");
            }
        }
    }
}