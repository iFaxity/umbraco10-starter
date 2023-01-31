using System.Globalization;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Guttew.Umbraco.Extensions;

public static class ContentExtensions
{
    /// <summary>
    /// Filters content which should not be visible to the user.
    /// </summary>
    public static IEnumerable<T> FilterForDisplay<T>(this IEnumerable<T> source)
        where T : IPublishedContent
    {
        // TODO: Should && x.TemplateId > 0 be added?
        return source.Where(x => x.IsVisible() && x.IsPublished());
    }

    public static CultureInfo? GetCultureInfo(this IPublishedContent content)
    {
        var culture = content.GetCultureFromDomains();

        return culture is null
            ? null
            : new CultureInfo(culture);
    }
}
