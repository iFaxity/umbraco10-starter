using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Guttew.Umbraco.Mvc;

public abstract class BlockComponentBase<TBlockData, TViewModel> : ViewComponent
    where TBlockData : IPublishedElement
    where TViewModel : IBlockViewModel<TBlockData>
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

    /// <summary>
    /// Sets viewName according to the Tag set in the component
    /// </summary>
    /// <param name="viewModel">ViewModel to render the view with</param>
    /// <typeparam name="T">View model type</typeparam>
    /// <returns></returns>
    public IViewComponentResult CurrentTemplate<T>(T viewModel)
    {
        return View(Tag, viewModel);
    }

    protected virtual IViewComponentResult InvokeComponent(TViewModel viewModel)
    {
        return CurrentTemplate(viewModel);
    }

    protected virtual Task<IViewComponentResult> InvokeComponentAsync(TViewModel viewModel)
    {
        return Task.FromResult(InvokeComponent(viewModel));
    }

    internal static TViewModel CreateViewModel(params IPublishedElement?[] arguments)
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
