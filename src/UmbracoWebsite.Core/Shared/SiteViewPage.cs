using Guttew.Umbraco.Html;
using Guttew.Umbraco.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.Views;

namespace UmbracoWebsite.Core.Shared;

/// <summary>
/// Wraps UmbracoViewPage but enforcing the model as IPageViewModel
/// </summary>
/// <typeparam name="TPageData"></typeparam>
/// <typeparam name="TViewModel"></typeparam>
public abstract class SiteViewPage<TPageData, TViewModel> : UmbracoViewPage<TViewModel>
    where TPageData : IPublishedContent
    where TViewModel : IPageViewModel<TPageData>
{
}

/// <summary>
/// Wraps UmbracoViewPage but enforcing the model as IPageViewModel
/// </summary>
/// <typeparam name="TPageData"></typeparam>
public abstract class SiteViewPage<TPageData> : SiteViewPage<TPageData, IPageViewModel<TPageData>>
    where TPageData : IPublishedContent
{
}

/// <summary>
/// Wraps UmbracoViewPage but enforcing the model as IPageViewModel
/// </summary>
public abstract class SiteViewPage : SiteViewPage<IPublishedContent>
{
}
