using Microsoft.AspNetCore.Mvc;
using MoneyWeb.Repository.Interfaces;

namespace MoneyWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("UsuarioForm");
        }
    }
}