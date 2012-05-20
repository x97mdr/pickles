using System.IO;
using NUnit.Framework;
using Ninject;
using Pickles.DirectoryCrawler;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenDeterminingRelevantFilesToConsider : BaseFixture
    {
        [Test]
        public void Then_can_detect_feature_files_successfully()
        {
            var relevantFileDetector = Kernel.Get<RelevantFileDetector>();
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new FileInfo("test.feature")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new FileInfo("test.markdown")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new FileInfo("test.mdown")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new FileInfo("test.mkdn")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new FileInfo("test.md")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new FileInfo("test.mdwn")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new FileInfo("test.mdtext")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new FileInfo("test.mdtxt")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new FileInfo("test.text")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new FileInfo("test.txt")));
            Assert.AreEqual(false, relevantFileDetector.IsRelevant(new FileInfo("test.pdf")));
            Assert.AreEqual(false, relevantFileDetector.IsRelevant(new FileInfo("test.doc")));
            Assert.AreEqual(false, relevantFileDetector.IsRelevant(new FileInfo("test.docx")));
        }
    }
}