using Microsoft.AspNetCore.Mvc.Rendering;

namespace MoneyWeb.Helpers
{
    public static class HtmlHelpers
    {
        public static string GetValidationCssClass(this IHtmlHelper htmlHelper, string propriedade)
        {
            var modelState = htmlHelper.ViewData.ModelState;

            if (modelState.ContainsKey(propriedade) && modelState[propriedade].Errors.Any())
                return "is-invalid";

            return string.Empty;
        }
    }
}