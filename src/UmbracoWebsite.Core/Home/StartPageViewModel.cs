using Guttew.Umbraco.Mvc;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace UmbracoWebsite.Core.Home;

public class StartPageViewModel : PageViewModel<StartPage>
{
    public StartPageViewModel(StartPage currentPage)
        : base(currentPage)
    {
    }
}
