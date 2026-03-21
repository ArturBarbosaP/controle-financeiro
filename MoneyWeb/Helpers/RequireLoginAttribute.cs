using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoneyWeb.Data;

namespace MoneyWeb.Helpers
{
    public class RequireLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool allowAnonymous = context.ActionDescriptor.EndpointMetadata.Any(x => x is AllowAnonymousAttribute);

            if (allowAnonymous)
                return;

            if (!context.HttpContext.Session.IsUsuarioLogado())
                context.Result = new RedirectToActionResult("Index", "Login", new {});
        }
    }
}