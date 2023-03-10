using Umbraco.Cms.Core.Models.PublishedContent;

namespace Guttew.Umbraco.Mvc;

public class PageViewModel<TPageData> : IPageViewModel<TPageData> where TPageData : IPublishedContent
{
    public TPageData CurrentPage { get; }

    public PageViewModel(TPageData currentPage)
    {
        CurrentPage = currentPage;
    }
}
