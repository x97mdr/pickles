using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Autofac;
using NFluent;
using NGenerics.DataStructures.Trees;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.DocumentationBuilders.HTML;
using PicklesDoc.Pickles.DocumentationBuilders.JSON;
using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.Test.DocumentationBuilders.HTML.AutomationLayer;
using TechTalk.SpecFlow;

namespace PicklesDoc.Pickles.Test.DocumentationBuilders.JSON.AutomationLayer
{
    [Binding]
    [Scope(Tag = "json")]
    public sealed class StepDefinitions : BaseFixture /* God object antipattern */
    {
        private GeneralTree<INode> nodes;
        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef

        [Given("I have this feature description")]
        public void IHaveThisFeatureDescription(string featureDescription)
        {
            FeatureParser parser = new FeatureParser(base.FileSystem);

            var feature = parser.Parse(new StringReader(featureDescription));

            this.nodes = new GeneralTree<INode>(new FeatureNode(base.FileSystem.DirectoryInfo.FromDirectoryName(@"c:\output\"), string.Empty, feature));
        }

        [When(@"I generate the documentation")]
        public void WhenIGenerateTheJsonDocumentation()
        {
            var configuration = base.Container.Resolve<Configuration>();
            configuration.OutputFolder = base.FileSystem.DirectoryInfo.FromDirectoryName(@"c:\output\");
            var jsonDocumentationBuilder = base.Container.Resolve<JSONDocumentationBuilder>();

            jsonDocumentationBuilder.Build(this.nodes);
        }

        [Then("the JSON file should contain")]
        public void ThenTheResultShouldBe(string expectedResult)
        {
            var actualResult = base.FileSystem.File.ReadAllText(@"c:\output\pickledFeatures.json");
            actualResult = actualResult.Replace("{", "{{").Replace("}", "}}");
            expectedResult = expectedResult.Replace("{", "{{").Replace("}", "}}");
            Check.That(actualResult).Contains(expectedResult);
        }
    }
}
