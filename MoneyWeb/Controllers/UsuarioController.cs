using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyWeb.Models.Entities;
using MoneyWeb.Models.ViewModels;
using MoneyWeb.Repository.Interfaces;

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

        public async Task<IActionResult> Index()
        {
            try
            {
                var usuarios = await _repository.GetUsuarios();
                var usuariosRead = _mapper.Map<IEnumerable<UsuarioViewModel>>(usuarios);
                return View(usuariosRead);
            }
            catch (Exception ex)
            {
                return ExibirViewErro($"Erro ao listar usuários: {ex.Message}");
            }
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Criar Usuário";
            ViewBag.Action = "Create";

            return View("UsuarioForm");
        }

        public async Task<IActionResult> Update(int id)
        {
            try
            {
                Usuario usuario = await _repository.GetUsuarioById(id) ?? throw new Exception($"Não existe nenhum usuário com o Id {id}");

                UsuarioViewModel usuarioUpdate = _mapper.Map<UsuarioViewModel>(usuario);

                ViewBag.Title = "Editar Usuário";
                ViewBag.Action = "Update";

                return View("UsuarioForm", usuarioUpdate);
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao editar usuário: {ex.Message}", false, "Index");
            }
        }

        public async Task<IActionResult> Read(int id)
        {
            try
            {
                Usuario usuario = await _repository.GetUsuarioById(id) ?? throw new Exception($"Não existe nenhum usuário com o Id {id}");

                UsuarioViewModel usuarioRead = _mapper.Map<UsuarioViewModel>(usuario);

                return View(usuarioRead);
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao visualizar usuário: {ex.Message}", false, "Index");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Usuario usuario = await _repository.GetUsuarioById(id) ?? throw new Exception($"Não existe nenhum usuário com o Id {id}");

                _repository.Delete(usuario);

                if (!await _repository.SaveChanges())
                    throw new Exception("Não foi possível excluir no banco de dados!");

                return ExibirMensagem("Usuário excluído com sucesso!", true, "Index");
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao excluir usuário: {ex.Message}", false, "Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioViewModel usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = "Criar Usuário";
                    ViewBag.Action = "Create";
                    return View("UsuarioForm", usuario);
                }

                Usuario usuarioInsert = _mapper.Map<Usuario>(usuario);
                _repository.Add(usuarioInsert);

                if (!await _repository.SaveChanges())
                    throw new Exception("Não foi possível criar no banco de dados!");

                return ExibirMensagem("Usuário criado com sucesso!", true, "Index");
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao criar usuário: {ex.Message}", false, "Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UsuarioViewModel usuarioViewModel)
        {
            try
            {
                Usuario usuario = await _repository.GetUsuarioById(usuarioViewModel.Id) ?? throw new Exception($"Não existe nenhum usuário com o Id {usuarioViewModel.Id}");

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = "Editar Usuário";
                    ViewBag.Action = "Update";

                    return View("UsuarioForm", usuarioViewModel);
                }

                Usuario usuarioUpdate = _mapper.Map(usuarioViewModel, usuario);
                _repository.Update(usuarioUpdate);

                if (!await _repository.SaveChanges())
                    throw new Exception("Não foi possível salvar no banco de dados!");

                return ExibirMensagem("Usuário salvo com sucesso!", true, "Index");

            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao editar usuário: {ex.Message}", false, "Index");
            }
        }
    }
}