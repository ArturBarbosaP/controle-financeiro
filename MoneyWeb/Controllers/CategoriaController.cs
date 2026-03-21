using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        private async Task<Categoria> GetCategoria(int id)
        {
            Usuario usuario = await _usuario;

            return usuario.Categorias.FirstOrDefault(c => c.Id == id) ?? throw new Exception("Você não tem permissão para editar essa categoria!");
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
                CategoriaViewModel categoriaUpdate = _mapper.Map<CategoriaViewModel>(await GetCategoria(id));

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
                CategoriaViewModel categoriaRead = _mapper.Map<CategoriaViewModel>(await GetCategoria(id));
                return View(categoriaRead);
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao visualizar categoria: {ex.Message}", false, "Index");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _repository.Delete(await GetCategoria(id));

                if (!await _repository.SaveChanges())
                    throw new Exception("Não foi possível excluir no banco de dados!");

                return ExibirMensagem("Categoria excluída com sucesso!", true, "Index");

            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao excluir categoria: {ex.Message}", false, "Index");
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

                Categoria categoria = await GetCategoria(categoriaViewModel.Id);

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