
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Advania.Umbraco.Menu;

public static class MenuHelper
{
    public static IHtmlContent FullMenu(this IHtmlHelper _, IPublishedContent rootNode, IPublishedContent currentNode, Func<MenuItem, IHtmlContent> itemTemplate)
    {
        if (rootNode is null)
            throw new ArgumentNullException("RootNode not set or null");

        var directChildren = rootNode?.Children?.Where(x => x.IsVisible() && x.TemplateId > 0 && x.IsPublished());
        var currentPath = currentNode.AncestorsOrSelf().Reverse().Select(x => x.Id);
        var items = directChildren?.Select(c => CreateMenuItem(c, currentNode, currentPath));

        var htmlContent = new HtmlContentBuilder();

        if (items is not null)
        {
            foreach (var item in items)
                htmlContent.AppendHtml(itemTemplate(item));
        }

        return htmlContent;
    }

    private static MenuItem CreateMenuItem(IPublishedContent content, IPublishedContent currentNode, IEnumerable<int> currentPath)
    {
        // check if the content is selected, either if it is the current content or in the hierarchy
        var isSelected = content.Id == currentNode.Id || currentPath.Contains(content.Id);

        // create lazy function to check if the content has children, will be filtered same way as the main function
        var hasChildren = new Lazy<bool>(() => content?.Children?.Where(x => x.IsVisible() && x.TemplateId > 0 && x.IsPublished())?.Any() ?? false);

        // create a new menu item from the content
        return new MenuItem(content, isSelected, hasChildren);
    }

    public class MenuItem
    {
        public IPublishedContent Content { get; init; }
        public bool IsSelected { get; init; }
        public Lazy<bool> HasChildren { get; init; }

        public MenuItem(IPublishedContent content, bool isSelected, Lazy<bool> hasChildren)
        {
            Content = content;
            IsSelected = isSelected;
            HasChildren = hasChildren;
        }
    }
}
