using Guttew.Umbraco.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace UmbracoWebsite.Core.Shared;

public class SitePageViewModel<TPageData> : PageViewModel<TPageData>, ISitePageViewModel<TPageData>
    where TPageData : IPublishedContent
{
    public SitePageViewModel(TPageData currentPage)
        : base(currentPage)
    {
        StartPage = CurrentPage.Root<StartPage>()!;
        Breadcrumbs = CurrentPage.Breadcrumbs();

        // Gets the section (tree ancestor directly under root)
        Section = CurrentPage.AncestorsOrSelf(maxLevel: 2)
            .LastOrDefault();
    }

    public StartPage StartPage { get; }
    public IPublishedContent? Section { get; }
    public IEnumerable<IPublishedContent> Breadcrumbs { get; }
}
