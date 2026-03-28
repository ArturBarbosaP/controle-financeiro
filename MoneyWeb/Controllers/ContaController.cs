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

        public IActionResult Create()
        {
            ViewBag.Title = "Criar Conta";
            ViewBag.Action = "Create";

            return View("ContaForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContaViewModel contaViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = "Criar Conta";
                    ViewBag.Action = "Create";

                    return View("ContaForm", contaViewModel);
                }

                Conta contaInsert = _mapper.Map<Conta>(contaViewModel);
                contaInsert.UsuarioId = this.UsuarioId;
                _repository.Add(contaInsert);

                if (!await _repository.SaveChanges())
                    throw new Exception("Não foi possível criar no banco de dados!");

                return ExibirMensagem("Conta criada com sucesso!", true, "Index");
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao criar conta: {ex.Message}", false, "Index");
            }
        }
    }
}