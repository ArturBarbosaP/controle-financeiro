using Microsoft.AspNetCore.Mvc;

namespace MoneyWeb.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}