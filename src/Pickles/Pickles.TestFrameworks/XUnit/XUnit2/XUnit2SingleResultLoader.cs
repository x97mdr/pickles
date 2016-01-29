using System;
using System.IO.Abstractions;

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.TestFrameworks.XUnit.XUnit2
{
    public class XUnit2SingleResultLoader : ISingleResultLoader
    {
        private readonly XmlDeserializer<assemblies> xmlDeserializer = new XmlDeserializer<assemblies>();

        public ITestResults Load(FileInfoBase fileInfo)
        {
            return new XUnit2SingleResults(this.xmlDeserializer.Load(fileInfo));
        }
    }
}