using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoneyWeb.Data;

namespace MoneyWeb.Helpers
{
    public class RequireLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Session.IsUsuarioLogado())
            {
                string returnUrl = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;

                context.Result = new RedirectToActionResult("Index", "Login", new { returnUrl = returnUrl });
            }
        }
    }
}