using System;

namespace Guttew.Optimizely.QueryString
{
    /// <summary>
    /// Extensions for QueryStringBuilderBase
    /// </summary>
    public static class QueryStringBuilderBaseExtensions
    {
        /// <summary>
        /// Adds query string parameter to query URL encoded if condition is met.
        /// </summary>
        /// <param name="builder">Instance of QueryStringBuilderBase.</param>
        /// <param name="condition">Condition.</param>
        /// <param name="name">Name of parameter.</param>
        /// <param name="value">Value of parameter.</param>
        /// <returns>Instance of modified QueryStringBuilderBase.</returns>
        public static IQueryStringBuilder AddIf(this IQueryStringBuilder builder, bool condition, string name, string value)
        {
            return condition ? builder.Add(name, value) : builder;
        }

        /// <summary>
        /// Adds query string parameter to query URL encoded if condition is met.
        /// </summary>
        /// <param name="builder">Instance of QueryStringBuilderBase.</param>
        /// <param name="condition">Condition.</param>
        /// <param name="name">Name of parameter.</param>
        /// <param name="value">Value of parameter.</param>
        /// <returns>Instance of modified QueryStringBuilderBase.</returns>
        public static IQueryStringBuilder AddIf(this IQueryStringBuilder builder, bool condition, string name, object value)
        {
            return condition ? builder.Add(name, value) : builder;
        }

        /// <summary>
        /// Adds query string parameter to query URL encoded if condition is met.
        /// </summary>
        /// <param name="builder">Instance of QueryStringBuilderBase.</param>
        /// <param name="condition">Condition.</param>
        /// <param name="name">Name of parameter.</param>
        /// <param name="action">Action to run if condition is true.</param>
        /// <returns>Instance of modified QueryStringBuilderBase.</returns>
        public static IQueryStringBuilder AddIf(this IQueryStringBuilder builder, bool condition, string name, Func<string> action)
        {
            return condition ? builder.Add(name, action()) : builder;
        }

        /// <summary>
        /// Adds a segment at the end of the URL encoded if condition is met.
        /// </summary>
        /// <param name="builder">Instance of QueryStringBuilderBase.</param>
        /// <param name="condition">Condition.</param>
        /// <param name="segment">Name of the segment.</param>
        /// <returns>Instance of modified QueryStringBuilderBase.</returns>
        public static IQueryStringBuilder AddSegmentIf(this IQueryStringBuilder builder, bool condition, string segment)
        {
            return condition ? builder.AddSegment(segment) : builder;
        }

        /// <summary>
        /// Removes query string parameter from query if condition is met.
        /// </summary>
        /// <param name="builder">Instance of QueryStringBuilderBase.</param>
        /// <param name="condition">Condition.</param>
        /// <param name="name">Name of parameter.</param>
        /// <returns>Instance of modified QueryStringBuilderBase.</returns>
        public static IQueryStringBuilder RemoveIf(this IQueryStringBuilder builder, bool condition, string name)
        {
            return condition ? builder.Remove(name) : builder;
        }
    }
}
