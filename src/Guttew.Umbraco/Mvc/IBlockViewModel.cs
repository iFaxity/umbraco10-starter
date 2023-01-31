using Umbraco.Cms.Core.Models.PublishedContent;

namespace Guttew.Umbraco.Mvc;

public interface IBlockViewModel<out TBlockData>
    where TBlockData : IPublishedElement
{
    TBlockData CurrentBlock { get; }
}

public interface IBlockViewModel<out TBlockData, out TSettings> : IBlockViewModel<TBlockData>
    where TBlockData : IPublishedElement
    where TSettings : IPublishedElement
{
    TSettings CurrentSettings { get; }
}
