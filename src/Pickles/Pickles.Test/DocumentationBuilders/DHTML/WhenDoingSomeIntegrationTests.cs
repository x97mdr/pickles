using NUnit.Framework;
using PicklesDoc.Pickles.DocumentationBuilders.DHTML;

namespace PicklesDoc.Pickles.Test.DocumentationBuilders.DHTML
{
    [TestFixture]
    public class WhenDoingSomeIntegrationTests : BaseFixture
    {
        [Test]
        public void TestTheResourceWriter()
        {
            var conf = new Configuration();
            conf.OutputFolder = FileSystem.DirectoryInfo.FromDirectoryName(@"d:\output");
            var resourceWriter = new DhtmlResourceWriter(FileSystem);
            resourceWriter.WriteTo(conf.OutputFolder.FullName);
        }

        [Test]
        public void CanAddFunctionWrapperAroundJson()
        {
            string filePath = @"d:\output\pickledFeatures.json";
            FileSystem.AddFile(filePath, "\r\n[]\r\n");

            var jsonTweaker = new JsonTweaker(FileSystem);
            jsonTweaker.AddJsonPWrapperTo(filePath);

            var expected = "jsonPWrapper (\r\n[]\r\n);";
            Assert.AreEqual(expected, FileSystem.File.ReadAllText(filePath));
        }

        [Test]
        public void CanRenameJsonFile()
        {
            string oldfilePath = @"d:\output\pickledFeatures.json";
            string newFilePath = @"d:\output\pickledFeatures.js";

            FileSystem.AddFile(oldfilePath, "test data");

            var jsonTweaker = new JsonTweaker(FileSystem);
            jsonTweaker.RenameFileTo(oldfilePath, newFilePath);

            Assert.IsTrue(FileSystem.File.Exists(newFilePath));
            Assert.IsFalse(FileSystem.File.Exists(oldfilePath));
        }
    }
}