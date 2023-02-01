using Guttew.Umbraco.Mvc;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Extensions;
using Models = Umbraco.Cms.Web.Common.PublishedModels;

namespace UmbracoWebsite.Core.StandardBlock;

[BlockComponent(typeof(Models.StandardBlock))]
public class StandardBlockComponent : BlockComponent<Models.StandardBlock, StandardBlockViewModel>
{
    protected override IViewComponentResult InvokeComponent(StandardBlockViewModel viewModel)
    {
        var culture = CurrentPage?.GetCultureFromDomains();

        return CurrentTemplate(viewModel);
    }
}
