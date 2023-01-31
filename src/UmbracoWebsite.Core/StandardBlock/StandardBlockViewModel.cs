using Guttew.Umbraco.Mvc;

namespace UmbracoWebsite.Core.StandardBlock;

public class StandardBlockViewModel : BlockViewModel<Umbraco.Cms.Web.Common.PublishedModels.StandardBlock>
{
    public StandardBlockViewModel(Umbraco.Cms.Web.Common.PublishedModels.StandardBlock block)
        : base(block)
    {
    }
}
