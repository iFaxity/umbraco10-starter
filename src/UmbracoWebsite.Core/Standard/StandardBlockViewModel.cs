using Guttew.Umbraco.Mvc;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace UmbracoWebsite.Core.Standard;

public class StandardBlockViewModel : BlockViewModel<StandardBlock>
{
    public StandardBlockViewModel(StandardBlock block)
        : base(block)
    {
    }
}
