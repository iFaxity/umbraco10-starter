using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;

namespace Guttew.Umbraco.Mvc;

public abstract class BlockComponentBase<TBlockData, TViewModel> : ViewComponent
    where TBlockData : IPublishedElement
    where TViewModel : IBlockViewModel<TBlockData>
{
    // TODO: Not sure if caching of UmbracoContext is necessary
    private IUmbracoContext? _umbracoContext;

    protected IUmbracoContext? UmbracoContext
    {
        get
        {
            if (_umbracoContext is null)
            {
                var umbracoContextAccessor = HttpContext.RequestServices.GetRequiredService<IUmbracoContextAccessor>();

                if (umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
                    _umbracoContext = umbracoContext;
            }

            return _umbracoContext;
        }
    }

    protected string? Tag { get; private set; }

    protected IPublishedContent? CurrentPage { get; private set; }

    internal void Init(string? tag)
    {
        Tag = tag;

        // Resolve the current page
        CurrentPage = ViewData.Model switch
        {
            IPublishedContent content => content,
            IContentModel model => model.Content,
            _ => UmbracoContext?.PublishedRequest?.PublishedContent,
        };
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
