using Guttew.Umbraco.Mvc;
using Models = Umbraco.Cms.Web.Common.PublishedModels;

namespace UmbracoWebsite.Core.StandardBlock;

public class StandardBlockViewModel : BlockViewModel<Models.StandardBlock>
{
    public StandardBlockViewModel(Models.StandardBlock block)
        : base(block)
    {
    }
}
