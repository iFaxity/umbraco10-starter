using Umbraco.Cms.Core.Models.PublishedContent;

namespace Advania.Optimizely.FolderList
{
    public static class FolderListExtensions
    {
        public static FolderItem CreateFolderItem(this IPublishedContent folder)
        {
            var documents = folder.Children
                .Select(x => new DocumentItem
                {
                    DocumentReference = x.ContentLink,
                    Name = GetName(x),
                    Mime = x.MimeType,
                })
                .OrderByDescending(x => x.Name)
                .ToList();

            var subFolders = folder.GetFolders()
                .OrderByDescending(n => n.Name)
                .ToList();

            return new FolderItem
            {
                Folder = folder,
                Documents = documents,
                SubFolders = subFolders,
            };
        }

        private static string GetName(MediaData media)
        {
            if (media.ContentLink.IsNullOrEmpty())
                return string.Empty;

            var titleCast = media as ITitle;

            if (!string.IsNullOrWhiteSpace(titleCast?.Title))
                return titleCast.Title;

            return media.Name;
        }
    }
}
