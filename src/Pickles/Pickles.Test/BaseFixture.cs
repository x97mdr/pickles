using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Reflection;

using Autofac;

using NUnit.Framework;

namespace PicklesDoc.Pickles.Test
{
    public class BaseFixture
    {
        protected const string FileSystemPrefix = @"c:\temp\FakeFolderStructures\";
        protected const string ResourcePrefix = "PicklesDoc.Pickles.Test.FakeFolderStructures.";
        private IContainer container;

        protected IContainer Container
        {
            get 
            {
                if (this.container == null)
                {
                    var builder = new ContainerBuilder();
                    builder.RegisterAssemblyTypes(typeof(Runner).Assembly);
                    builder.Register<MockFileSystem>(_ => new MockFileSystem()).As<IFileSystem>().SingleInstance();
                    builder.RegisterModule<PicklesModule>();
                    this.container = builder.Build();
                }

                return this.container;
            }
        }

        protected MockFileSystem MockFileSystem
        {
            get { return (MockFileSystem)this.Container.Resolve<IFileSystem>(); }
        }

        [TearDown]
        public void TearDown()
        {
            if (this.container != null)
                this.container.Dispose();
            this.container = null;
        }


      protected void AddFakeFolderStructures()
      {
          AddFakeFolderAndFiles("AcceptanceTest", new[] { "AdvancedFeature.feature", "LevelOne.feature" });
          AddFakeFolderAndFiles("EmptyFolderTests", new string[0]);

          AddFakeFolderAndFiles("FeatureCrawlerTests", new[] { "index.md", "LevelOne.feature" });
          AddFakeFolderAndFiles(@"FeatureCrawlerTests\SubLevelOne", new[] { "ignorethisfile.ignore", "LevelOneSublevelOne.feature", "LevelOneSublevelTwo.feature" });
          AddFakeFolderAndFiles(@"FeatureCrawlerTests\SubLevelOne\SubLevelTwo", new[] { "LevelOneSublevelOneSubLevelTwo.feature" });
          AddFakeFolderAndFiles(@"FeatureCrawlerTests\SubLevelOne\SubLevelTwo\IgnoreThisDirectory", new[] { "IgnoreThisFile.ignore" });
      }

      protected void AddFakeFolderAndFiles(string directoryName, IEnumerable<string> fileNames)
      {
          string directoryPath = FileSystemPrefix + directoryName + @"\";
          string resourceIdentifier = ResourcePrefix + directoryName.Replace(@"\", ".") + ".";

          MockFileSystem.AddDirectory(directoryPath);

          foreach (var fileName in fileNames)
          {
              MockFileSystem.AddFile(
                  directoryPath + fileName,
                  RetrieveContentOfFileFromResources(resourceIdentifier + fileName));
          }
      }

        protected static string RetrieveContentOfFileFromResources(string resourceName)
        {
            string resultFile;

            System.IO.Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

            using (var reader = new System.IO.StreamReader(manifestResourceStream))
            {
                resultFile = reader.ReadToEnd();
            }

            return resultFile;
        }
    }
}