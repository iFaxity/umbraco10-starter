using Guttew.Umbraco.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace UmbracoWebsite.Core.Shared;

public interface ISitePageViewModel<out TPageData> : IPageViewModel<TPageData>
    where TPageData : IPublishedContent
{
    public StartPage StartPage { get; }
    public IPublishedContent? Section { get; }
    public IEnumerable<IPublishedContent> Breadcrumbs { get; }
}
