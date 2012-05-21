using System.IO;
using NUnit.Framework;
using Ninject;
using Pickles.DirectoryCrawler;
using Should;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenDeterminingRelevantFilesToConsider : BaseFixture
    {
        [Test]
        public void ThenCanDetectFeatureFilesSuccessfully()
        {
            var relevantFileDetector = Kernel.Get<RelevantFileDetector>();
            relevantFileDetector.IsRelevant(new FileInfo("test.feature")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(new FileInfo("test.markdown")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(new FileInfo("test.mdown")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(new FileInfo("test.mkdn")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(new FileInfo("test.md")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(new FileInfo("test.mdwn")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(new FileInfo("test.mdtext")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(new FileInfo("test.mdtxt")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(new FileInfo("test.text")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(new FileInfo("test.txt")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(new FileInfo("test.pdf")).ShouldBeFalse();
            relevantFileDetector.IsRelevant(new FileInfo("test.doc")).ShouldBeFalse();
            relevantFileDetector.IsRelevant(new FileInfo("test.docx")).ShouldBeFalse();
        }
    }
}