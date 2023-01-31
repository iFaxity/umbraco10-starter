using System.Text.Encodings.Web;
using System.Web;
using Advania.Optimizely.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Advania.Optimizely.QueryString
{
    /// <summary>
    ///     Helper class for creating and modifying URL's QueryString.
    /// </summary>
    public class QueryStringBuilder : IQueryStringBuilder, IHtmlContent
    {
        /// <summary>
        ///     Represents the empty query string. Field is read-only.
        /// </summary>
        public static readonly QueryStringBuilder Empty = Create(string.Empty);

        /// <summary>
        ///     Factory method for instantiating new QueryStringBuilder with provided URL.
        /// </summary>
        /// <param name="url">URL for which to build query.</param>
        /// <returns>Instance of QueryStringBuilder.</returns>
        public static QueryStringBuilder Create(string url)
        {
            return new QueryStringBuilder(url);
        }

        /// <summary>
        ///     Factory method for instantiating new QueryStringBuilder with provided URL.
        /// </summary>
        /// <param name="contentReference">Content for which to build query.</param>
        /// <returns>Instance of QueryStringBuilder.</returns>
        public static QueryStringBuilder Create(IPublishedContent content)
        {
            return new QueryStringBuilder(content);
        }

        private UriBuilder _uriBuilder;
        private QueryBuilder _queryBuilder;

        /// <summary>
        ///     Instantiates new QueryStringBuilder with provided URL.
        /// </summary>
        /// <param name="url">URL for which to build query.</param>
        protected QueryStringBuilder(string url)
        {
            _uriBuilder = new UriBuilder(url);
            _queryBuilder = new QueryBuilder();
        }

        /// <summary>
        ///     Instantiates new QueryStringBuilder with provided URL.
        /// </summary>
        /// <param name="contentReference">ContentReference for which to build query.</param>
        protected QueryStringBuilder(IPublishedContent content)
            : base()
        {
            _uriBuilder = new UriBuilder(content.Url());
        }

        /// <inheritdoc />
        public Uri Url
        {
            get => _uriBuilder.Uri;
        }

        /// <summary>
        ///     Adds query string parameter to query URL encoded.
        /// </summary>
        /// <param name="name">Name of parameter.</param>
        /// <param name="value">Value of parameter.</param>
        /// <returns>Instance of modified QueryStringBuilder.</returns>
        public IQueryStringBuilder Add(string name, string value)
        {
            if (!string.IsNullOrEmpty(value))
                _queryBuilder.Add(name, value);

            return this;
        }

        /// <inheritdoc />
        public IQueryStringBuilder Add(string name, object value)
        {
            if (value == null)
                return this;

            return Add(name, value.ToString());
        }

        public IQueryStringBuilder AddRange(string name, IEnumerable<string?> values)
        {
            var source = values.Where(value => !string.IsNullOrEmpty(value));

            if (source.Any())
                _queryBuilder.Add(name, source);

            return this;
        }

        public IQueryStringBuilder AddRange(string name, IEnumerable<object?> values)
        {
            var source = values.Select(value => value?.ToString());

            return AddRange(name, source);
        }

        /// <inheritdoc />
        public IQueryStringBuilder AddSegment(string segment)
        {
            _uriBuilder.Path = _uriBuilder.Path.AppendTrailingSlash() + HttpUtility.UrlPathEncode(segment.TrimStart('/'));
            return this;
        }

        /// <inheritdoc />
        public IQueryStringBuilder Remove(string name)
        {
            _uriBuilder.QueryCollection.Remove(name);
            return this;
        }

        /// <inheritdoc />
        public IQueryStringBuilder Toggle(string name, string value)
        {
            if (string.IsNullOrEmpty(value))
                return this;

            var currentValue = HttpUtility.UrlDecode(_uriBuilder.QueryCollection[name]);

            if (currentValue != null && currentValue == value)
                Remove(name);
            else
                Add(name, value);

            return this;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var builder = new UriBuilder(_uriBuilder.Uri)
            {
                Query = _queryBuilder.ToString(),
            };

            return builder.ToString();
        }

        /// <inheritdoc />
        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            encoder.Encode(writer, ToString());
        }
    }
}
