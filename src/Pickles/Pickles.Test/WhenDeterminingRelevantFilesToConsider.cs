using System;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.DirectoryCrawler;
using Should;

namespace PicklesDoc.Pickles.Test
{
    [TestFixture]
    public class WhenDeterminingRelevantFilesToConsider : BaseFixture
    {
        [Test]
        public void ThenCanDetectFeatureFilesSuccessfully()
        {
            var relevantFileDetector = Container.Resolve<RelevantFileDetector>();
            relevantFileDetector.IsRelevant(MockFileSystem.FileInfo.FromFileName("test.feature")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(MockFileSystem.FileInfo.FromFileName("test.markdown")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(MockFileSystem.FileInfo.FromFileName("test.mdown")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(MockFileSystem.FileInfo.FromFileName("test.mkdn")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(MockFileSystem.FileInfo.FromFileName("test.md")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(MockFileSystem.FileInfo.FromFileName("test.mdwn")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(MockFileSystem.FileInfo.FromFileName("test.mdtext")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(MockFileSystem.FileInfo.FromFileName("test.mdtxt")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(MockFileSystem.FileInfo.FromFileName("test.text")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(MockFileSystem.FileInfo.FromFileName("test.txt")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(MockFileSystem.FileInfo.FromFileName("test.pdf")).ShouldBeFalse();
            relevantFileDetector.IsRelevant(MockFileSystem.FileInfo.FromFileName("test.doc")).ShouldBeFalse();
            relevantFileDetector.IsRelevant(MockFileSystem.FileInfo.FromFileName("test.docx")).ShouldBeFalse();
        }
    }
}