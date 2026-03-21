using Microsoft.AspNetCore.Mvc;
using MoneyWeb.Data;
using MoneyWeb.Helpers;
using MoneyWeb.Models.Entities;
using MoneyWeb.Models.ViewModels;
using MoneyWeb.Repository.Interfaces;
using System.Diagnostics;

namespace MoneyWeb.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IUsuarioRepository _repository;

        public LoginController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logoff()
        {
            HttpContext.Session.ClearUsuarioLogado();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel login)
        {
            if (Debugger.IsAttached)
            {
                HttpContext.Session.SetUsuarioLogado(1, "Artur");
                return RedirectToAction("Index", "Home");
            }

            try
            {
                if (!ModelState.IsValid)
                    return View(login);

                Usuario user = await _repository.GetUsuarioByUsuario(login.Usuario);

                if (user == null || !PasswordHelper.VerifyPassword(login.Senha, user.Senha))
                {
                    ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos!");
                    return View(login);
                }

                HttpContext.Session.SetUsuarioLogado(user.Id, user.Nome);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(login);
            }
        }
    }
}