using Guttew.Umbraco.Mvc;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace UmbracoWebsite.Core.Standard;

[BlockComponent(typeof(StandardBlockViewComponent))]
public class StandardBlockViewComponent : BlockComponent<StandardBlock, StandardBlockViewModel>
{
    protected override IViewComponentResult InvokeComponent(StandardBlockViewModel viewModel)
    {
        return View(viewModel);
    }
}
