using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace COVID19.Statistics.Web
{
    public static class HtmlExtensions
    {
        public static IHtmlContent DisabledIf(this IHtmlHelper htmlHelper, bool condition)
        => new HtmlString(condition ? "disabled" : "");
    }
}
