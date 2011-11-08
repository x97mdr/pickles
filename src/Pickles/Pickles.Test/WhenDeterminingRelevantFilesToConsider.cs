using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using NUnit.Framework;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenDeterminingRelevantFilesToConsider : BaseFixture
    {
        [Test]
        public void Then_can_detect_feature_files_successfully()
        {
            var relevantFileDetector = Kernel.Get<RelevantFileDetector>();
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new System.IO.FileInfo("test.feature")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new System.IO.FileInfo("test.markdown")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new System.IO.FileInfo("test.mdown")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new System.IO.FileInfo("test.mkdn")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new System.IO.FileInfo("test.md")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new System.IO.FileInfo("test.mdwn")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new System.IO.FileInfo("test.mdtext")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new System.IO.FileInfo("test.mdtxt")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new System.IO.FileInfo("test.text")));
            Assert.AreEqual(true, relevantFileDetector.IsRelevant(new System.IO.FileInfo("test.txt")));
            Assert.AreEqual(false, relevantFileDetector.IsRelevant(new System.IO.FileInfo("test.pdf")));
            Assert.AreEqual(false, relevantFileDetector.IsRelevant(new System.IO.FileInfo("test.doc")));
            Assert.AreEqual(false, relevantFileDetector.IsRelevant(new System.IO.FileInfo("test.docx")));
        }
    }
}
