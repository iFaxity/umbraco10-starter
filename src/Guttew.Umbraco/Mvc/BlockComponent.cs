using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Guttew.Umbraco.Mvc;

public abstract class BlockComponentBase<TViewModel> : ViewComponent
{
    protected string? Tag { get; private set; }

    protected IPublishedContent? CurrentPage { get; private set; }

    internal void Init(string? tag)
    {
        // Set tag
        Tag = tag;

        // Load the CurrentPage from the ViewData
        if (ViewData.Model is IPublishedContent currentPage)
            CurrentPage = currentPage;
    }

    protected virtual IViewComponentResult InvokeComponent(TViewModel viewModel)
    {
        return View(viewModel);
    }

    protected virtual Task<IViewComponentResult> InvokeComponentAsync(TViewModel viewModel)
    {
        return Task.FromResult(InvokeComponent(viewModel));
    }

    protected TViewModel CreateViewModel<TBlockData>(BlockListItem<TBlockData> currentBlock)
        where TBlockData : IPublishedElement
    {
        return CreateViewModel(currentBlock.Content);
    }

    protected TViewModel CreateViewModel<TBlockData>(BlockGridItem<TBlockData> currentBlock)
        where TBlockData : IPublishedElement
    {
        return CreateViewModel(currentBlock.Content);
    }

    protected TViewModel CreateViewModel<TBlockData, TSettingsData>(BlockListItem<TBlockData, TSettingsData> model)
        where TBlockData : IPublishedElement
        where TSettingsData : IPublishedElement
    {
        return CreateViewModel(model.Content, model.Settings);
    }

    protected TViewModel CreateViewModel<TBlockData, TSettingsData>(BlockGridItem<TBlockData, TSettingsData> model)
        where TBlockData : IPublishedElement
        where TSettingsData : IPublishedElement
    {
        return CreateViewModel(model.Content, model.Settings);
    }

    private TViewModel CreateViewModel(params IPublishedElement?[] arguments)
    {
        TViewModel? viewModel;

        try
        {
            viewModel = (TViewModel?)Activator.CreateInstance(typeof(TViewModel), arguments);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"View model of type '{typeof(TViewModel)}' is invalid", ex);
        }

        if (viewModel is null)
            throw new InvalidOperationException($"View model of type '{typeof(TViewModel)}' could not be instantiated!");

        return viewModel;
    }
}

public abstract class BlockComponent<TBlockData, TViewModel> : BlockComponentBase<TViewModel>
    where TBlockData : IPublishedElement
    where TViewModel : BlockViewModel<TBlockData>
{
    public virtual Task<IViewComponentResult> InvokeAsync(IBlockReference currentBlock, string? tag = null)
    {
        Init(tag);

        var viewModel = currentBlock switch
        {
            BlockGridItem<TBlockData> gridItem => CreateViewModel(gridItem),
            BlockListItem<TBlockData> listItem => CreateViewModel(listItem),
            _ => throw new InvalidOperationException("")
        };

        return InvokeComponentAsync(viewModel);
    }
}

public abstract class BlockComponent<TBlockData, TSettingsData, TViewModel> : BlockComponentBase<TViewModel>
    where TBlockData : IPublishedElement
    where TSettingsData : IPublishedElement
    where TViewModel : IBlockViewModel<TBlockData, TSettingsData>
{
    public Task<IViewComponentResult> InvokeAsync(IBlockReference currentBlock, string? tag = null)
    {
        Init(tag);

        var viewModel = currentBlock switch
        {
            BlockGridItem<TBlockData, TSettingsData> gridItem => CreateViewModel(gridItem),
            BlockListItem<TBlockData, TSettingsData> listItem => CreateViewModel(listItem),
            _ => throw new InvalidOperationException("")
        };

        return InvokeComponentAsync(viewModel);
    }
}
