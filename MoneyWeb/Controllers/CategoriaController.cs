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

        private Task<Usuario> _usuario
        {
            get
            {
                return _usuarioRepository.GetUsuarioById(UsuarioId);
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
                Usuario usuario = await _usuario;

                if (usuario == null)
                    return DeslogarUsuario();

                var categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(usuario.Categorias);
                return View(categorias);
            }
            catch (Exception ex)
            {
                return ExibirViewErro($"Erro ao listar categorias: {ex.Message}");
            }
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Criar Categoria";
            ViewBag.Action = "Create";

            return View("CategoriaForm");
        }

        public async Task<IActionResult> Update(int id)
        {
            try
            {
                Usuario usuario = await _usuario;

                if (usuario == null)
                    return DeslogarUsuario();

                var categoria = usuario.Categorias.FirstOrDefault(c => c.Id == id) ?? throw new Exception("Você não tem permissão para editar essa categoria!");

                CategoriaViewModel categoriaUpdate = _mapper.Map<CategoriaViewModel>(categoria);

                ViewBag.Title = "Editar Categoria";
                ViewBag.Action = "Update";

                return View("CategoriaForm", categoriaUpdate);
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao editar categoria: {ex.Message}", false, "Index");
            }
        }

        public async Task<IActionResult> Read(int id)
        {
            try
            {
                Usuario usuario = await _usuario;

                if (usuario == null)
                    return DeslogarUsuario();

                var categoria = usuario.Categorias.FirstOrDefault(c => c.Id == id) ?? throw new Exception("Você não tem permissão para editar essa categoria!");

                if (categoria == null)
                    return ExibirMensagem("Erro ao visualizar categoria: Você não tem permissão para visualizar essa categoria!", false, "Index");

                CategoriaViewModel categoriaRead = _mapper.Map<CategoriaViewModel>(categoria);

                return View(categoriaRead);
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao visualizar categoria: {ex.Message}", false, "Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaViewModel categoriaViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = "Criar Categoria";
                    ViewBag.Action = "Create";

                    return View("CategoriaForm", categoriaViewModel);
                }

                Categoria categoriaInsert = _mapper.Map<Categoria>(categoriaViewModel);
                categoriaInsert.UsuarioId = this.UsuarioId;
                _repository.Add(categoriaInsert);

                if (!await _repository.SaveChanges())
                    throw new Exception("Não foi possível criar no banco de dados!");

                return ExibirMensagem("Categoria criada com sucesso!", true, "Index");
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao criar categoria: {ex.Message}", false, "Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategoriaViewModel categoriaViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = "Editar Categoria";
                    ViewBag.Action = "Update";

                    return View("CategoriaForm", categoriaViewModel);
                }

                Categoria categoria = await _repository.GetCategoriaById(categoriaViewModel.Id) ?? throw new Exception("Você não tem permissão para editar essa categoria!");

                Categoria categoriaUpdate = _mapper.Map(categoriaViewModel, categoria);
                _repository.Update(categoriaUpdate);

                if (!await _repository.SaveChanges())
                    throw new Exception("Não foi possível salvar no banco de dados!");

                return ExibirMensagem("Categoria salva com sucesso!", true, "Index");
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao salvar categoria: {ex.Message}", false, "Index");
            }
        }
    }
}