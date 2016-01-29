using System.IO.Abstractions;

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.TestFrameworks.XUnit
{
    public interface ISingleResultLoader
    {
        ITestResults Load(FileInfoBase fileInfo);
    }
}