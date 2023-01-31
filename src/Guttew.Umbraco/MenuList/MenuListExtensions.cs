using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Advania.Optimizely.MenuList;

public static class MenuListExtensions
{
    public static IEnumerable<MenuItem> GetMenuItems(this IPublishedContent root, IPublishedContent current, bool requireVisibleInMenu, bool requireTemplate = true, bool includeRoot = false)
    {
        // fetch all children
        var children = root.Children ?? Enumerable.Empty<IPublishedContent>();

        // add the root item, if set to include it
        if (includeRoot)
            children.Prepend(root);

        // get the path of the current content
        var contentPath = current
            .Ancestors()
            .Reverse()
            .SkipWhile(x => x != root)
            .ToList();

        // convert all IContent to MenuItems to prepare for rendering
        return children.Select(c => CreateMenuItem(c, current, contentPath, requireTemplate, requireVisibleInMenu));
    }

    /// <summary>
    /// Creates a menu item from the specified content.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="currentContentLink">The current content link.</param>
    /// <param name="contentPath">The content path.</param>
    /// <param name="requireTemplate">if set to <c>true</c> [require template].</param>
    /// <param name="requireVisibleInMenu">if set to <c>true</c> [require visible in menu].</param>
    /// <returns></returns>
    private static MenuItem CreateMenuItem(IPublishedContent content, IPublishedContent currentContentLink, List<IPublishedContent> contentPath, bool requireTemplate, bool requireVisibleInMenu)
    {
        // create a new menu item from the content
        var isExact = content == currentContentLink;

        return new MenuItem
        {
            // sets the content
            Content = content,
            IsSelected = isExact || contentPath.Contains(content),
            IsExact = isExact,
            // create lazy function to check if the content has children, will be filtered same way as the main function
            HasChildren = new Lazy<bool>(() => content.Children?.Any() == true),
        };
    }
}
