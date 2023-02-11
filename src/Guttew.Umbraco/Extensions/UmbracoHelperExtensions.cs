using Umbraco.Cms.Web.Common.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Core.Models.PublishedContent;
using System.Globalization;

namespace Guttew.Umbraco.Extensions;

public static class UmbracoHelperExtensions
{
    private static ILocalizationService? _localizationService;
    private static ILocalizationService LocalizationService
        => _localizationService ??= StaticServiceProvider.Instance.GetRequiredService<ILocalizationService>();

    public static string? GetSpecificDictionaryValue(this UmbracoHelper _, string key, string? culture)
    {
        if (culture is null)
            return null;

        var item = LocalizationService.GetDictionaryItemByKey(key);

        var translation = item?.Translations.FirstOrDefault(x => culture.Equals(x.Language?.IsoCode, StringComparison.InvariantCultureIgnoreCase));

        return translation?.Value;
    }

    public static string? GetSpecificDictionaryValue(this UmbracoHelper helper, string key, PublishedCultureInfo? culture)
    {
        return GetSpecificDictionaryValue(helper, key, culture?.Culture);
    }

    public static string? GetSpecificDictionaryValue(this UmbracoHelper _, string key, CultureInfo? cultureInfo)
    {
        if (cultureInfo is null)
            return null;

        var item = LocalizationService.GetDictionaryItemByKey(key);

        var translation = item?.Translations.FirstOrDefault(x => cultureInfo.Equals(x.Language?.CultureInfo));

        return translation?.Value;
    }
}
