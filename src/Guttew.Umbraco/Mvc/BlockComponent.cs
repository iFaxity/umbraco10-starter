using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Guttew.Umbraco.Mvc;

public abstract class BlockComponent<TBlockData, TViewModel> : BlockComponentBase<TBlockData, TViewModel>
    where TBlockData : IPublishedElement
    where TViewModel : BlockViewModel<TBlockData>
{
    public Task<IViewComponentResult> InvokeAsync(IBlockReference currentBlock, string? tag = null)
    {
        Init(tag);

        var viewModel = currentBlock switch
        {
            BlockGridItem<TBlockData> gridItem => CreateViewModel(gridItem.Content),
            BlockListItem<TBlockData> listItem => CreateViewModel(listItem.Content),
            _ => throw new InvalidOperationException("")
        };

        return InvokeComponentAsync(viewModel);
    }
}

public abstract class BlockComponent<TBlockData, TSettingsData, TViewModel> : BlockComponentBase<TBlockData, TViewModel>
    where TBlockData : IPublishedElement
    where TSettingsData : IPublishedElement
    where TViewModel : IBlockViewModel<TBlockData, TSettingsData>
{
    public Task<IViewComponentResult> InvokeAsync(IBlockReference currentBlock, string? tag = null)
    {
        Init(tag);

        var viewModel = currentBlock switch
        {
            BlockGridItem<TBlockData, TSettingsData> gridItem => CreateViewModel(gridItem.Content, gridItem.Settings),
            BlockListItem<TBlockData, TSettingsData> listItem => CreateViewModel(listItem.Content, listItem.Settings),
            _ => throw new InvalidOperationException("")
        };

        return InvokeComponentAsync(viewModel);
    }
}

