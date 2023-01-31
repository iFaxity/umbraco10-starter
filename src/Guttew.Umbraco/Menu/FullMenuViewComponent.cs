using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace Advania.Umbraco.Menu;

[ViewComponent(Name = "FullMenu")]
public class FullMenuViewComponent : ViewComponent
{
    public FullMenuViewComponent()
    {
    }

    public IViewComponentResult Invoke(FullMenuViewModel viewModel)
    {
        var directChildren = viewModel.Root?.Children?.Where(x => x.IsVisible() && x.TemplateId > 0 && x.IsPublished() && x.GetType() != typeof(Article));
        var currentPath = viewModel.Current?.AncestorsOrSelf().Reverse().Select(x => x.Id);

        viewModel.MenuItems = directChildren
            ?.Select(c => CreateMenuItem(c, viewModel.Current, currentPath));

        return View(viewModel);
    }

    private MenuItem CreateMenuItem(IPublishedContent content, IPublishedContent? currentNode, IEnumerable<int>? currentPath)
    {
        // create a new menu item from the content
        return new MenuItem()
        {
            // sets the content
            Content = content,

            // check if the content is selected, either if it is the current content or in the hierarchy
            IsSelected = content.Id == currentNode?.Id || (currentPath?.Contains(content.Id) == true),

            // create lazy function to check if the content has children, will be filtered same way as the main function
            HasChildren = new Lazy<bool>(() => content?.Children?.Where(x => x.IsVisible() && x.TemplateId > 0 && x.IsPublished() && x.GetType() != typeof(Article))?.Any() ?? false),
        };
    }
}
