using Umbraco.Cms.Core.Models.PublishedContent;

namespace Guttew.Umbraco.Menu;

public class FullMenuViewModel
{
    public IPublishedContent? Root { get; set; }
    public IPublishedContent? Current { get; set; }
    public IEnumerable<MenuItem>? MenuItems { get; set; }
    public int MenuLevel { get; set; }
}
