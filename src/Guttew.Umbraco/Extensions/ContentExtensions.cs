using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.DependencyInjection;
using Umbraco.Extensions;

namespace Guttew.Umbraco.Extensions;


public static class ContentExtensions
{
    private static ILocalizationService? _localizationService;
    private static ILocalizationService LocalizationService
        => _localizationService ??= StaticServiceProvider.Instance.GetRequiredService<ILocalizationService>();

    /// <summary>
    /// Filters content which should not be visible to the user.
    /// </summary>
    public static IEnumerable<T> FilterForDisplay<T>(this IEnumerable<T> source)
        where T : IPublishedContent
    {
        // TODO: Should && x.TemplateId > 0 be added?
        return source.Where(x => x.IsVisible() && x.IsPublished());
    }


    public static ILanguage? GetLanguage(this PublishedCultureInfo? publishedCulture)
    {
        return LocalizationService.GetLanguageByIsoCode(publishedCulture?.Culture);
    }

    /// <summary>
    /// Parses the <see cref="PublishedCultureInfo"/> into a <seealso cref="CultureInfo"/>.
    /// </summary>
    /// <param name="publishedCulture"></param>
    /// <returns></returns>
    public static CultureInfo? GetCultureInfo(this PublishedCultureInfo? publishedCulture)
    {
        return ParseCultureInfo(publishedCulture?.Culture);
    }

    public static PublishedCultureInfo? GetCurrentCulture(this IPublishedContent content)
    {
        var culture = content.GetCultureFromDomains();

        if (culture is null)
            return null;

        if (!content.Cultures.TryGetValue(culture, out var publishedCulture))
            return null;

        return publishedCulture;
    }

    public static CultureInfo? GetCurrentCultureInfo(this IPublishedContent content)
    {
        var publishedCulture = GetCurrentCulture(content);

        return GetCultureInfo(publishedCulture);
    }

    private static CultureInfo? ParseCultureInfo(string? culture)
    {
        CultureInfo? cultureInfo = null;

        if (culture is not null)
        {
            try
            {
                cultureInfo = new CultureInfo(culture);
            }
            catch (CultureNotFoundException)
            {
                // ignore
            }
        }

        return cultureInfo;
    }
}
