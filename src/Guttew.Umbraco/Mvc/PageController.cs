using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Guttew.Umbraco.Mvc;

public abstract class PageController<TPageData, TViewModel> : RenderController
        where TPageData : PublishedContentModel
        where TViewModel : IPageViewModel<TPageData>
{
    protected override TPageData? CurrentPage
    {
        get => (TPageData?)base.CurrentPage;
    }

    public PageController(
        ILogger<RenderController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor
    ) : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
    }

    public override IActionResult Index()
    {
        return View(CreateViewModel(CurrentPage));
    }

    protected virtual IActionResult View(TViewModel viewModel)
    {
        return CurrentTemplate(viewModel);
    }

    /// <summary>
    /// Creates a specified view model for the page content model.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    /// <param name="currentPage"></param>
    /// <returns></returns>
    protected TViewModel CreateViewModel([NotNullIfNotNull("currentPage")] TPageData? currentPage)
    {
        ArgumentNullException.ThrowIfNull(currentPage);

        try
        {
            return CreateViewModel<TViewModel>(currentPage);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Model of type '{typeof(TPageData).Name}' is invalid", ex);
        }
    }

    /// <summary>
    /// Creates a specified view model for the current page.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="currentPage"></param>
    /// <returns></returns>
    protected T CreateViewModel<T>(TPageData currentPage)
        where T : IPageViewModel<TPageData>
    {
        ArgumentNullException.ThrowIfNull(currentPage);

        try
        {
            var viewModel = (T?)Activator.CreateInstance(typeof(T), currentPage);

            if (viewModel is null)
                throw new NullReferenceException($"View model of type '{typeof(T)}' could not be instantiated!");

            return viewModel;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Could not create view model for page {typeof(TPageData)}", ex);
        }
    }
}
