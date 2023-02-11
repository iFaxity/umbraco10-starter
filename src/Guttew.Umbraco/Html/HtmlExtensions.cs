using System.Reflection;
using Guttew.Umbraco.Mvc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Guttew.Umbraco.Html;

public static class HtmlExtensions
{
    public static Task<IHtmlContent> GetBlockGridComponentsHtmlAsync(this IViewComponentHelper helper, BlockGridModel model, string? tag = null)
    {
        return GetBlockComponentsHtmlAsync(helper, model, tag);
    }

    public static Task<IHtmlContent> GetBlockListComponentsHtmlAsync(this IViewComponentHelper helper, BlockListModel model, string? tag = null)
    {
        return GetBlockComponentsHtmlAsync(helper, model, tag);
    }

    public static Task<IHtmlContent> GetBlockComponentHtmlAsync(
        this IViewComponentHelper helper,
        IBlockReference<IPublishedElement, IPublishedElement> currentBlock,
        string? tag = null)
    {
        var type = GetBlockViewComponentType(currentBlock);

        if (type is null)
            throw new InvalidOperationException($"No BlockComponent found for block {currentBlock.Content.GetType()}");

        return helper.InvokeAsync(type, new
        {
            currentBlock,
            tag,
        });
    }

    private static Type? GetBlockViewComponentType(IBlockReference<IPublishedElement, IPublishedElement> block)
    {
        // TODO Move this to a helper?
        var modelAlias = block.Content.ContentType.Alias;

        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsSubclassOf(typeof(ViewComponent)))
            .Where(x =>
            {
                var attr = x.GetCustomAttribute<BlockComponentAttribute>();

                return attr?.Name == modelAlias;
            })
            .FirstOrDefault();
    }

    private static async Task<IHtmlContent> GetBlockComponentsHtmlAsync(
        this IViewComponentHelper helper,
        IEnumerable<IBlockReference<IPublishedElement, IPublishedElement>> blocks,
        string? tag)
    {
        var builder = new HtmlContentBuilder();

        foreach (var currentBlock in blocks)
        {
            if (currentBlock?.Content is null)
                continue;

            // TODO: Add more optional parameters
            var content = await GetBlockComponentHtmlAsync(helper, currentBlock, tag);

            builder.AppendHtml(content);
        }

        return builder;
    }
}
