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
            var unzipper = new UnZipper(MockFileSystem);
            unzipper.UnZip(@"D:\features\Pickles.Examples\BaseDhtmlFiles.zip", @"d:\output", "BaseDhtmlFiles");
        }

        [Test]
        [Ignore]
        public void TestTheResourceWriter()
        {
            var conf = new Configuration();
            conf.OutputFolder = MockFileSystem.DirectoryInfo.FromDirectoryName(@"d:\output");
            var resourceWriter = new DhtmlResourceProcessor(conf, new DhtmlResourceSet(conf, MockFileSystem), MockFileSystem);
            resourceWriter.WriteZippedResources();
        }

        [Test]
        [Ignore]
        public void CanAddFunctionWrapperAroundJson()
        {
            var jsonTweaker = new JsonTweaker(MockFileSystem);
            string filePath = @"d:\output\pickledFeatures.json";
            jsonTweaker.AddJsonPWrapperTo(filePath);

            var expected = "jsonPWrapper (\r\n[]\r\n);";
            Assert.AreEqual(expected, MockFileSystem.File.ReadAllText(filePath));
        }

        [Test]
        [Ignore]
        public void CanRenameJsonFile()
        {
            var jsonTweaker = new JsonTweaker(MockFileSystem);
            string oldfilePath = @"d:\output\pickledFeatures.json";
            string newFilePath = @"d:\output\pickledFeatures.js";
            jsonTweaker.RenameFileTo(oldfilePath, newFilePath);

            Assert.IsTrue(MockFileSystem.File.Exists(newFilePath));
            Assert.IsFalse(MockFileSystem.File.Exists(oldfilePath));
        }
    }
}