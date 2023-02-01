using Umbraco.Cms.Core.Models;

namespace Guttew.Umbraco.FolderList;

public class DocumentItem
{
    public IContent DocumentReference { get; set; }

    public string Name { get; set; }

    public string Mime { get; set; }

    public DateTime Date { get; set; }

    public DocumentItem()
    {
    }
}
