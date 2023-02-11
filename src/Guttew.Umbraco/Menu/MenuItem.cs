using Umbraco.Cms.Core.Models.PublishedContent;

namespace Guttew.Umbraco.Menu;

public class MenuItem
{
    public IPublishedContent? Content { get; internal set; }
    public bool IsSelected { get; internal set; }
    public Lazy<bool>? HasChildren { get; internal set; }
}
