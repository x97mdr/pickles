using System;
namespace Pickles.Parser
{
    public interface IFeatureElement
    {
        string Description { get; set; }
        Feature Feature { get; set; }
        string Name { get; set; }
        System.Collections.Generic.List<Step> Steps { get; set; }
        System.Collections.Generic.List<string> Tags { get; set; }
    }
}
