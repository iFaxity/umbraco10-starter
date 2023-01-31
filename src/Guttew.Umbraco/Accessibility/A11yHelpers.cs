using System.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Guttew.Umbraco.Accessibility;

public static class A11yHelpers
{
    #region A11yLink
    public static IHtmlContent A11yLink(this IHtmlHelper helper, Link item)
        => A11yLink(helper, item.Url, item.Name, item.Target);

    public static IHtmlContent A11yLink(this IHtmlHelper helper, MediaWithCrops media, string? title = null, string? target = null)
        => A11yLink(helper, media.MediaUrl(), title, target);

    public static IHtmlContent A11yLink(this IHtmlHelper helper, IPublishedContent content, string? title = null, string? target = null)
        => A11yLink(helper, content.Url(), title, target);

    public static IHtmlContent A11yLink(this IHtmlHelper helper, string? href, string? title = null, string? target = null)
    {
        // if target is set add rel=noopener noreferrer
        var attributes = new Dictionary<string, string>();

        if (!string.IsNullOrEmpty(href))
            attributes.Add("href", href);

        if (!string.IsNullOrEmpty(title))
        {
            attributes.Add("title", title);
            attributes.Add("aria-label", title);
        }

        if (!string.IsNullOrEmpty(target))
        {
            attributes.Add("target", target);
            attributes.Add("rel", "noopener noreferrer");
        }

        return MapAttributes(helper, attributes);
    }
    #endregion

    #region A11yCardLink
    public static IHtmlContent A11yCardLink(this IHtmlHelper helper, Link item)
        => A11yLink(helper, item.Url, item.Name, item.Target);

    public static IHtmlContent A11yCardLink(this IHtmlHelper helper, MediaWithCrops media, string? title = null, string? target = null)
        => A11yLink(helper, media.MediaUrl(), title, target);

    public static IHtmlContent A11yCardLink(this IHtmlHelper helper, IPublishedContent content, string? title = null, string? target = null)
        => A11yLink(helper, content.Url(), title, target);

    public static IHtmlContent A11yCardLink(this IHtmlHelper helper, string href, string? title = null, string? target = null)
    {
        var attributes = new Dictionary<string, string>();

        if (!string.IsNullOrEmpty(href))
            attributes.Add("data-link", href);

        if (!string.IsNullOrEmpty(title))
        {
            attributes.Add("title", title);
            attributes.Add("aria-label", title);
        }

        if (!string.IsNullOrEmpty(target))
            attributes.Add("data-target", target);

        return MapAttributes(helper, attributes);
    }
    #endregion

    #region MapAttributes
    public static IHtmlContent MapAttributes<TValue>(this IHtmlHelper _, object obj)
    {
        var builder = new HtmlContentBuilder();
        var props = obj.GetType()
            .GetProperties()
            .Where(x => x.CanRead == true);

        foreach (var prop in props)
        {
            var value = prop.GetValue(obj, null);
            if (value != null)
            {
                if (builder.Count == 0)
                    builder.Append(" ");

                builder.AppendFormat("{0}=\"{1}\"", prop.Name, HttpUtility.HtmlAttributeEncode(value.ToString()));
            }
        }

        if (builder.Count == 0)
            return HtmlString.Empty;

        return builder;
    }

    public static IHtmlContent MapAttributes<TKey, TValue>(this IHtmlHelper _, IDictionary<TKey, TValue> dict)
    {
        var builder = new HtmlContentBuilder();

        foreach (var pair in dict)
        {
            var key = pair.Key?.ToString();

            if (pair.Value != null && !string.IsNullOrEmpty(key))
            {
                if (builder.Count == 0)
                    builder.Append(" ");

                builder.AppendFormat("{0}=\"{1}\"", key, HttpUtility.HtmlAttributeEncode(pair.Value.ToString()));
            }
        }

        if (builder.Count == 0)
            return HtmlString.Empty;

        return builder;
    }

    public static IHtmlContent MapAttributes<TValue>(this IHtmlHelper _, IList<TValue> list)
    {
        var builder = new HtmlContentBuilder();

        foreach (var item in list)
        {
            var key = item?.ToString();

            if (!string.IsNullOrEmpty(key))
            {
                if (builder.Count == 0)
                    builder.Append(" ");

                builder.AppendFormat("{0}=\"\"", key);
            }
        }

        if (builder.Count == 0)
            return HtmlString.Empty;

        return builder;
    }
    #endregion
}
