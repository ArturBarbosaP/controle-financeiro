using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyWeb.Helpers;
using MoneyWeb.Models.Entities;
using MoneyWeb.Models.ViewModels;
using MoneyWeb.Repository.Interfaces;

namespace MoneyWeb.Controllers
{
    [RequireLogin]
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
                if (string.IsNullOrEmpty(usuario.Senha))
                    ModelState.AddModelError(nameof(UsuarioViewModel.Senha), "Digite a senha!");

                if (string.IsNullOrEmpty(usuario.ConfirmarSenha))
                    ModelState.AddModelError(nameof(UsuarioViewModel.ConfirmarSenha), "Confirme a senha!");

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
            catch (DbUpdateException ex) when (ex.InnerException.Message.Contains("USUARIO_UNIQUE"))
            {
                ModelState.AddModelError(nameof(UsuarioViewModel.NomeUsuario), "Este usuário já está em uso!");
                ViewBag.Title = "Criar Usuário";
                ViewBag.Action = "Create";
                return View("UsuarioForm", usuario);
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
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = "Editar Usuário";
                    ViewBag.Action = "Update";

                    return View("UsuarioForm", usuarioViewModel);
                }

                Usuario usuario = await _repository.GetUsuarioById(usuarioViewModel.Id) ?? throw new Exception($"Não existe nenhum usuário com o Id {usuarioViewModel.Id}");

                Usuario usuarioUpdate = _mapper.Map(usuarioViewModel, usuario);
                _repository.Update(usuarioUpdate);

                if (!await _repository.SaveChanges())
                    throw new Exception("Não foi possível salvar no banco de dados!");

                return ExibirMensagem("Usuário salvo com sucesso!", true, "Index");

            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao salvar usuário: {ex.Message}", false, "Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(UsuarioViewModel usuarioViewModel)
        {
            try
            {
                ModelState.Remove(nameof(UsuarioViewModel.Nome));
                ModelState.Remove(nameof(UsuarioViewModel.NomeUsuario));

                if (string.IsNullOrEmpty(usuarioViewModel.SenhaAtual))
                    ModelState.AddModelError(nameof(UsuarioViewModel.SenhaAtual), "Digite a senha atual!");

                if (string.IsNullOrEmpty(usuarioViewModel.Senha))
                    ModelState.AddModelError(nameof(UsuarioViewModel.Senha), "Digite a nova senha!");

                if (string.IsNullOrEmpty(usuarioViewModel.ConfirmarSenha))
                    ModelState.AddModelError(nameof(UsuarioViewModel.ConfirmarSenha), "Confirme a senha!");

                if (!ModelState.IsValid)
                {
                    TempData["AbrirModalSenha"] = true;
                    return View("Read", usuarioViewModel);
                }

                Usuario usuario = await _repository.GetUsuarioById(usuarioViewModel.Id) ?? throw new Exception($"Não existe nenhum usuário com o Id {usuarioViewModel.Id}");

                if (!PasswordHelper.VerifyPassword(usuarioViewModel.SenhaAtual, usuario.Senha))
                {
                    ModelState.AddModelError(nameof(UsuarioViewModel.SenhaAtual), "A senha atual está incorreta!");
                    TempData["AbrirModalSenha"] = true;
                    return View("Read", usuarioViewModel);
                }

                Usuario usuarioUpdate = _mapper.Map(usuarioViewModel, usuario);
                _repository.Update(usuarioUpdate);

                if (!await _repository.SaveChanges())
                    throw new Exception("Não foi possível salvar no banco de dados!");

                return ExibirMensagem("Senha alterada com sucesso!", true, "Index");
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao alterar senha do usuário: {ex.Message}", false, "Index");
            }
        }
    }
}