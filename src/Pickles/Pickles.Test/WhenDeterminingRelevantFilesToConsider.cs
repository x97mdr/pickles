using System;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.DirectoryCrawler;
using NFluent;

namespace PicklesDoc.Pickles.Test
{
    [TestFixture]
    public class WhenDeterminingRelevantFilesToConsider : BaseFixture
    {
        [Test]
        public void ThenCanDetectFeatureFilesSuccessfully()
        {
            var relevantFileDetector = Container.Resolve<RelevantFileDetector>();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.feature"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.markdown"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.mdown"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.mkdn"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.md"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.mdwn"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.mdtext"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.mdtxt"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.text"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.txt"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.pdf"))).IsFalse();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.doc"))).IsFalse();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.docx"))).IsFalse();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.docx"))).IsFalse();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("myproject.csproj.FileListAbsolute.txt"))).IsFalse();
        }
    }
}