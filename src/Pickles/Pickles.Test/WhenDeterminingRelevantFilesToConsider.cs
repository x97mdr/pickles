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
            relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.feature")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.markdown")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.mdown")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.mkdn")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.md")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.mdwn")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.mdtext")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.mdtxt")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.text")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.txt")).ShouldBeTrue();
            relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.pdf")).ShouldBeFalse();
            relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.doc")).ShouldBeFalse();
            relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.docx")).ShouldBeFalse();
        }
    }
}