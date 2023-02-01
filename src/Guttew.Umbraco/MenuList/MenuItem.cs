using Umbraco.Cms.Core.Models.PublishedContent;

namespace Guttew.Umbraco.MenuList;

/// <summary>
/// Class to represent each item in a menu list.
/// </summary>
public class MenuItem
{
    /// <summary>
    /// Gets the current content of the menu item.
    /// </summary>
    public IPublishedContent Content { get; internal set; }

    /// <summary>
    /// Gets a value indicating whether this menu item is selected.
    /// </summary>
    public bool IsSelected { get; internal set; }

    /// <summary>
    /// Gets a value indicating whether this menu item is selected.
    /// </summary>
    public bool IsExact { get; internal set; }

    /// <summary>
    /// Gets a value indicating whether this menu item is has children.
    /// </summary>
    public Lazy<bool> HasChildren { get; internal set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuItem"/> class.
    /// </summary>
    internal MenuItem()
    {
    }
}
