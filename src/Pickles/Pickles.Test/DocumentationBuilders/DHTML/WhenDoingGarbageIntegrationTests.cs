using NUnit.Framework;
using PicklesDoc.Pickles.DocumentationBuilders.DHTML;

namespace PicklesDoc.Pickles.Test.DocumentationBuilders.DHTML
{
    [TestFixture]
    public class WhenDoingGarbageIntegrationTests : BaseFixture
    {
        [Test]
        public void TestTheUnzipper_ShouldNotThrowException()
        {
            var unzipper = new UnZipper(MockFileSystem);
            unzipper.UnZip(@"D:\features\Pickles.Examples\BaseDhtmlFiles.zip", @"d:\output", "BaseDhtmlFiles");
        }

        [Test]
        public void TestTheResourceWriter()
        {
            var conf = new Configuration();
            conf.OutputFolder = MockFileSystem.DirectoryInfo.FromDirectoryName(@"d:\output");
            var resourceWriter = new DhtmlResourceProcessor(conf, new DhtmlResourceSet(conf, MockFileSystem), MockFileSystem);
            resourceWriter.WriteZippedResources();
        }

        [Test]
        public void CanAddFunctionWrapperAroundJson()
        {
            string filePath = @"d:\output\pickledFeatures.json";
            MockFileSystem.AddFile(filePath, "\r\n[]\r\n");

            var jsonTweaker = new JsonTweaker(MockFileSystem);
            jsonTweaker.AddJsonPWrapperTo(filePath);

            var expected = "jsonPWrapper (\r\n[]\r\n);";
            Assert.AreEqual(expected, MockFileSystem.File.ReadAllText(filePath));
        }

        [Test]
        public void CanRenameJsonFile()
        {
            string oldfilePath = @"d:\output\pickledFeatures.json";
            string newFilePath = @"d:\output\pickledFeatures.js";

            MockFileSystem.AddFile(oldfilePath, "test data");

            var jsonTweaker = new JsonTweaker(MockFileSystem);
            jsonTweaker.RenameFileTo(oldfilePath, newFilePath);

            Assert.IsTrue(MockFileSystem.File.Exists(newFilePath));
            Assert.IsFalse(MockFileSystem.File.Exists(oldfilePath));
        }
    }
}