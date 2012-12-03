using System.IO;
using NUnit.Framework;
using PicklesDoc.Pickles.DocumentationBuilders.DHTML;

namespace PicklesDoc.Pickles.Test.DocumentationBuilders.DHTML
{
    [TestFixture]
    public class WhenDoingGarbageIntegrationTests : BaseFixture
    {
        [Test]
        [Ignore]
        public void TestTheUnzipper()
        {
            var unzipper = new UnZipper();
            unzipper.UnZip(@"D:\features\Pickles.Examples\BaseDhtmlFiles.zip", @"d:\output", "BaseDhtmlFiles");
        }

        [Test]
        [Ignore]
        public void TestTheResourceWriter()
        {
            var conf = new Configuration();
            conf.OutputFolder = new DirectoryInfo(@"d:\output");
            var resourceWriter = new DhtmlResourceWriter(conf, new DhtmlResourceSet(conf));
            resourceWriter.WriteZippedResources();
        }
    }
}