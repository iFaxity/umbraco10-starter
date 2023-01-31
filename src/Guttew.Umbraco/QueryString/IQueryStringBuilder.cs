namespace Guttew.Optimizely.QueryString;

public interface IQueryStringBuilder
{
    /// <summary>
    ///     Gets the current Uri from the QueryStringBuilder
    /// </summary>
    Uri Url { get; }

    /// <summary>
    ///     Adds query string parameter to query URL encoded.
    /// </summary>
    /// <param name="name">Name of parameter.</param>
    /// <param name="value">Value of parameter.</param>
    /// <returns>Instance of modified QueryStringBuilder.</returns>
    IQueryStringBuilder Add(string name, string value);

    /// <summary>
    ///     Adds query string parameter to query URL encoded.
    /// </summary>
    /// <param name="name">Name of parameter.</param>
    /// <param name="value">Value of parameter.</param>
    /// <returns>Instance of modified QueryStringBuilder.</returns>
    IQueryStringBuilder Add(string name, object value);

    /// <summary>
    ///     Adds a segment at the end of the URL.
    /// </summary>
    /// <param name="segment">Name of the segment</param>
    /// <returns>Instance of modified QueryStringBuilder.</returns>
    IQueryStringBuilder AddSegment(string segment);

    /// <summary>
    ///     Removes query string parameter from query.
    /// </summary>
    /// <param name="name">Name of parameter to remove.</param>
    /// <returns>Instance of modified QueryStringBuilder.</returns>
    IQueryStringBuilder Remove(string name);

    /// <summary>
    ///     Adds query string parameter to query string if it is not already present, otherwise it removes it.
    /// </summary>
    /// <param name="name">Name of parameter to add or remove.</param>
    /// <param name="value">Value of parameter to add.</param>
    /// <returns>Instance of modified QueryStringBuilder.</returns>
    IQueryStringBuilder Toggle(string name, string value);
}
