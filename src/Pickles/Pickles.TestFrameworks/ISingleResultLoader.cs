using System.IO.Abstractions;

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.TestFrameworks
{
    public interface ISingleResultLoader
    {
        ITestResults Load(FileInfoBase fileInfo);
    }
}