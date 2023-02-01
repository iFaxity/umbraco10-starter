using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Guttew.Umbraco.Extensions;

public static class StringExtensions
{
    private const string Ellipsis = "…";
    private static readonly Regex KEBAB_CASE_REGEX = new("(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z0-9])", RegexOptions.Compiled);

    /// <summary>
    ///     Returns a number of characters from the start of the string followed by a '...'.
    /// </summary>
    /// <param name="source">Source string.</param>
    /// <param name="headLength">Number of characters to get from the string.</param>
    /// <returns>string value with first characters from the string followed by a '...'.</returns>
    public static string GetHead(this string source, int headLength)
    {
        if (source.EndsWith(Environment.NewLine))
            headLength -= Environment.NewLine.Length;

        if (source.Length <= headLength)
            return source;

        var result = source[..(headLength - Ellipsis.Length)];

        if (!result.EndsWith(Ellipsis))
            result += Ellipsis;

        if (source.EndsWith(Environment.NewLine))
            result += Environment.NewLine;

        return result;
    }

    /// <summary>
    ///     Returns a number of characters from the end of the string with a '...' at the beginning.
    /// </summary>
    /// <param name="source">Source string.</param>
    /// <param name="tailLength">Number of characters to get from the string.</param>
    /// <returns>string value with last characters from the string with a '...' at the beginning.</returns>
    public static string GetTail(this string source, int tailLength)
    {
        if (source.Length <= tailLength)
            return source;

        var result = source[(source.Length - tailLength + Ellipsis.Length)..];

        if (!result.StartsWith(Ellipsis))
            result = Ellipsis + result;

        return result;
    }

    /// <summary>
    /// Checks if the string is either null or empty.
    /// </summary>
    /// <param name="input">The string to check.</param>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? input)
    {
        return string.IsNullOrEmpty(input);
    }

    /// <summary>
    /// Checks if the string is either null or whitespace.
    /// </summary>
    /// <param name="input">The string to check.</param>
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? input)
    {
        return string.IsNullOrWhiteSpace(input);
    }

    /// <summary>
    /// Returns fallback string if input is null or empty.
    /// </summary>
    /// <param name="input">The string to check.</param>
    /// <param name="fallback">Fallback string.</param>
    public static string DefaultIfEmpty(this string? input, string fallback)
    {
        return IsNullOrEmpty(input) ? fallback : input;
    }

    /// <summary>
    /// Returns fallback string if input is null or whitespace.
    /// </summary>
    /// <param name="input">The string to check.</param>
    /// <param name="fallback">Fallback string.</param>
    public static string DefaultIfWhiteSpace(this string? input, string fallback)
    {
        return IsNullOrWhiteSpace(input) ? fallback : input;
    }

    /// <summary>
    /// Encodes the string as HTML.
    /// </summary>
    /// <param name="input">The dangerous string to encode.</param>
    /// <returns>The safely encoded HTML string.</returns>
    public static string HtmlEncode(this string input)
    {
        return !IsNullOrEmpty(input) ? WebUtility.HtmlEncode(input) : input;
    }

    /// <summary>
    /// Decodes an HTML string.
    /// </summary>
    /// <param name="input">The HTML string to decode.</param>
    /// <returns>The decoded HTML string.</returns>
    public static string HtmlDecode(this string input)
    {
        return !IsNullOrEmpty(input) ? WebUtility.HtmlDecode(input) : input;
    }

    /// <summary>
    /// Encodes the string for URLs.
    /// </summary>
    /// <param name="input">The dangerous string to URL encode.</param>
    /// <returns>The safely encoded URL string.</returns>
    public static string UrlEncode(this string input)
    {
        return !IsNullOrEmpty(input) ? WebUtility.UrlEncode(input) : input;
    }

    /// <summary>
    /// Decodes a URL-encoded string.
    /// </summary>
    /// <param name="input">The URL-encoded string to decode.</param>
    /// <returns>The decoded string.</returns>
    public static string UrlDecode(this string input)
    {
        return !IsNullOrEmpty(input) ? WebUtility.UrlDecode(input) : input;
    }

    /// <summary>
    /// Checks if a string is absolute URL.
    /// </summary>
    /// <param name="url">The string to check.</param>
    /// <returns>Returns true, if it is absolute URL and false when not.</returns>
    public static bool IsAbsoluteUrl(this string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var _);
    }

    /// <summary>
    /// Checks if a string is relative URL.
    /// </summary>
    /// <param name="url">The string to check.</param>
    /// <returns>Returns true, if it is relative URL and false when not.</returns>
    public static bool IsRelativeUrl(this string url)
    {
        return Uri.TryCreate(url, UriKind.Relative, out var _);
    }

    /// <summary>
    /// Changes a string to title case.
    /// </summary>
    /// <param name="input">Source string.</param>
    /// <param name="culture">Specific culture to use when capitalizing text.</param>
    /// <returns>string value with title case.</returns>
    public static string Capitalize(this string input, CultureInfo? culture = null)
    {
        if (IsNullOrEmpty(input))
            return input;

        culture ??= Thread.CurrentThread.CurrentCulture;
        return culture.TextInfo.ToTitleCase(input.ToLower());
    }

    /// <summary>
    /// Converts a camelCase or PascalCase string to kebab-case
    /// </summary>
    /// <param name="value">Source string.</param>
    /// <returns>string in kebab-case</returns>
    public static string ToKebabCase(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        return KEBAB_CASE_REGEX.Replace(value, "-$1")
            .Trim()
            .ToLower();
    }

    /// <summary>
    /// Replaces line breaks (\n) to &lt;br /&gt; elements.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string NewLineToBr(this string value)
    {
        if (IsNullOrEmpty(value))
            return string.Empty;

        return value.Replace(Environment.NewLine, "<br/>");
    }

    /// <summary>
    /// Determines whether string is a valid email address.
    /// </summary>
    /// <param name="mailAddress">The mail string.</param>
    /// <returns></returns>
    public static bool IsValidEmail(this string mailAddress)
    {
        return MailAddress.TryCreate(mailAddress, out _);
    }

    /// <summary>
    /// Appends trailing slash to path if it doesn't already exist
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string AppendTrailingSlash(this string path)
    {
        if (IsNullOrEmpty(path) || path.EndsWith("/"))
            return path;

        path += "/";
        return path;
    }
}
