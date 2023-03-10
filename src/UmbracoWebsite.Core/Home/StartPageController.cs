using Guttew.Umbraco.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace UmbracoWebsite.Core.Home;

public class StartPageController : PageController<StartPage, StartPageViewModel>
{
    public StartPageController(
        ILogger<RenderController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
    }

    public IActionResult Index()
    {
        var viewModel = CreateViewModel(CurrentPage);

        return CurrentTemplate(viewModel);
    }
}
