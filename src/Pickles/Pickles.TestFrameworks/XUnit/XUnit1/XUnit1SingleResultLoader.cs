using System;
using System.IO.Abstractions;

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.TestFrameworks.XUnit.XUnit1
{
    public class XUnit1SingleResultLoader : ISingleResultLoader
    {
        private static readonly XDocumentLoader DocumentLoader = new XDocumentLoader();

        public ITestResults Load(FileInfoBase fileInfo)
        {
            return new XUnit1SingleResult(DocumentLoader.Load(fileInfo));
        }
    }
}