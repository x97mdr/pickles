using System;
namespace Pickles
{
    public interface IFeatureParser<T>
    {
        T Parse(System.IO.TextReader featureFileReader);
    }
}
