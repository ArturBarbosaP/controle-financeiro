using Microsoft.AspNetCore.Mvc;
using MoneyWeb.Models.ViewModels;

namespace MoneyWeb.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult ExibirMensagem(string mensagem, bool sucesso, string action, string controller = null)
        {
            TempData[sucesso ? "Sucesso" : "Erro"] = mensagem;

            if (controller != null && controller != string.Empty)
                return RedirectToAction(action, controller);

            return RedirectToAction(action);
        }

        public IActionResult ExibirViewErro(string mensagem)
        {
            return View("Error", new ErrorViewModel { RequestId = mensagem });
        }
    }
}