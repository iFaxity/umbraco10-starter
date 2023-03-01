using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;

namespace Guttew.Umbraco.Html;

public static class ImageHelper
{
    public static IHtmlContent Image(this IHtmlHelper helper, MediaWithCrops? media, string? title = null, string? altText = null, string? cssClass = null)
    {
        return Image(helper, media?.MediaUrl(), title, altText, cssClass);
    }

    public static IHtmlContent Image(this IHtmlHelper _, string? url, string? title = null, string? altText = null, string? cssClass = null)
    {
        var builder = new TagBuilder("img");

        builder.MergeAttribute("src", url);

        if (!altText.IsNullOrWhiteSpace())
        {
            builder.MergeAttribute("alt", altText);
            builder.MergeAttribute("aria-label", title);
        }

        if (!title.IsNullOrWhiteSpace())
        {
            builder.MergeAttribute("title", title);
            builder.MergeAttribute("aria-label", title, replaceExisting: true);
        }

        if (!cssClass.IsNullOrWhiteSpace())
            builder.MergeAttribute("class", cssClass);

        return builder.RenderSelfClosingTag();
    }
}
