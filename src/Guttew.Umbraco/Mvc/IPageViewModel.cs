using Umbraco.Cms.Core.Models.PublishedContent;

namespace Guttew.Umbraco.Mvc;

public interface IPageViewModel<out TPageData>
    where TPageData : IPublishedContent
{
    /// <summary>
    /// Gets the current page.
    /// </summary>
    /// <value>
    /// The current page.
    /// </value>
    TPageData CurrentPage { get; }
}
