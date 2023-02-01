using Umbraco.Cms.Core.Models;

namespace Guttew.Umbraco.FolderList;

public class FolderItem
{
    public IContent Folder { get; internal set; }

    public IEnumerable<DocumentItem> Documents { get; set; }

    public IEnumerable<IContent> SubFolders { get; internal set; }

    public FolderItem()
    {
    }
}
