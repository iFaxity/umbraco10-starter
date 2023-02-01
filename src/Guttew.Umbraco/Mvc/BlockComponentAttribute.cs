using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Guttew.Umbraco.Mvc;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class BlockComponentAttribute : ViewComponentAttribute
{
    public const string ComponentPrefix = "umbviewcomponent";

    public BlockComponentAttribute(Type blockType)
    {
        var attr = blockType.GetCustomAttribute<PublishedModelAttribute>();
        if (attr is null)
            throw new InvalidOperationException($"Block type {blockType} is not a Umbraco generated Model!");

        Name = $"{ComponentPrefix}_{attr.ContentTypeAlias}";
    }
}
