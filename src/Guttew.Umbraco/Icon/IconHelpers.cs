using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Guttew.Umbraco.Icon;

public static class IconHelpers
{
    public static IHtmlContent Icon(
        this IHtmlHelper helper,
        string icon,
        string? className = null,
        string? id = null,
        string? title = null)
    {
        return helper.Partial($"Icons/_{icon}", new IconViewModel(className, id, title));
    }

    public static Task<IHtmlContent> IconAsync(
        this IHtmlHelper helper,
        string? icon,
        string? className = null,
        string? id = null,
        string? title = null)
    {
        return helper.PartialAsync($"Icons/_{icon}", new IconViewModel(className, id, title));
    }
}
