using Microsoft.AspNetCore.Mvc;
using MoneyWeb.Data;

namespace MoneyWeb.Controllers
{
    public class LoginController : BaseController
    {
        public IActionResult Index(string? returnUrl)
        {
#if DEBUG
                HttpContext.Session.SetUsuarioLogado(1, "Artur");
#endif
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logoff()
        {
            HttpContext.Session.ClearUsuarioLogado();

            return RedirectToAction("Index", "Home");
        }
    }
}