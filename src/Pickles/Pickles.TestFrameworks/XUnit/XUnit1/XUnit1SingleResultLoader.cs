using System;
using System.IO.Abstractions;

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.TestFrameworks.XUnit.XUnit1
{
    public class XUnit1SingleResultLoader : ISingleResultLoader
    {
        private readonly XDocumentLoader documentLoader = new XDocumentLoader();

        public ITestResults Load(FileInfoBase fileInfo)
        {
            return new XUnit1SingleResult(this.documentLoader.Load(fileInfo));
        }
    }
}