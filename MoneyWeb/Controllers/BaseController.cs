using Microsoft.AspNetCore.Mvc;

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
    }
}