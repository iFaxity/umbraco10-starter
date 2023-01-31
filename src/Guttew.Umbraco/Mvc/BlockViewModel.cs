using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Guttew.Umbraco.Mvc;

public class BlockViewModel<TBlockData> : IBlockViewModel<TBlockData>
    where TBlockData : IPublishedElement
{
    public TBlockData CurrentBlock { get; }

    public BlockViewModel(TBlockData block)
    {
        CurrentBlock = block;
    }
}

public class BlockViewModel<TBlockData, TSettings> : BlockViewModel<TBlockData>, IBlockViewModel<TBlockData, TSettings>
    where TBlockData : IPublishedElement
    where TSettings : IPublishedElement
{
    public TSettings CurrentSettings { get; }

    public BlockViewModel(TBlockData block, TSettings settings)
        : base(block)
    {
        CurrentSettings = settings;
    }
}
