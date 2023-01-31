using Guttew.Umbraco.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.Views;

namespace Guttew.Umbraco.Html;

public abstract class BasicViewPage<TPageData, TViewModel> : UmbracoViewPage<TViewModel>
    where TPageData : IPublishedContent
    where TViewModel : IPageViewModel<TPageData>
{
}

public abstract class BasicViewPage<TPageData> : BasicViewPage<TPageData, IPageViewModel<TPageData>>
    where TPageData : IPublishedContent
{
}

public abstract class BasicViewPage : BasicViewPage<IPublishedContent>
{
}
