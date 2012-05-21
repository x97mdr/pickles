#region License

/*
    Copyright [2011] [Jeffrey Cameron]

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

#endregion

using NUnit.Framework;
using Ninject;
using Pickles.DocumentationBuilders.DITA;
using Pickles.DocumentationBuilders.Excel;
using Pickles.DocumentationBuilders.Word;
using Should;

namespace Pickles.Test
{
    public class WhenResolvingADocumentationBuilder : BaseFixture
    {
        [Test]
        public void Then_can_resolve_IDocumentationBuilder_as_HtmlDocumentationBuilder_if_the_user_selects_HTML_output()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.DocumentationFormat = DocumentationFormat.Html;

            var item = Kernel.Get<IDocumentationBuilder>();

            item.ShouldNotBeNull();
            item.ShouldBeType<HtmlDocumentationBuilder>();
        }

        [Test]
        public void ThenCanResolveIDocumentationBuilderAsHtmlDocumentationBuilderAsSingletonIfTheUserSelectsHtmlOutput()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.DocumentationFormat = DocumentationFormat.Html;

            var item1 = Kernel.Get<IDocumentationBuilder>();
            var item2 = Kernel.Get<IDocumentationBuilder>();

            item1.ShouldNotBeNull();
            item1.ShouldBeType<HtmlDocumentationBuilder>();
            item2.ShouldNotBeNull();
            item2.ShouldBeType<HtmlDocumentationBuilder>();
            item1.ShouldBeSameAs(item2);
        }

        [Test]
        public void ThenCanResolveIDocumentationBuilderAsWordDocumentationBuilderIfTheUserSelectsWordOutput()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.DocumentationFormat = DocumentationFormat.Word;

            var item = Kernel.Get<IDocumentationBuilder>();

            item.ShouldNotBeNull();
            item.ShouldBeType<WordDocumentationBuilder>();
        }

        [Test]
        public void ThenCanResolveIDocumentationBuilderAsWordDocumentationBuilderAsSingletonIfTheUserSelectsWordOutput()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.DocumentationFormat = DocumentationFormat.Word;

            var item1 = Kernel.Get<IDocumentationBuilder>();
            var item2 = Kernel.Get<IDocumentationBuilder>();

            item1.ShouldNotBeNull();
            item1.ShouldBeType<WordDocumentationBuilder>();
            item2.ShouldNotBeNull();
            item2.ShouldBeType<WordDocumentationBuilder>();
            item1.ShouldBeSameAs(item2);
        }

        [Test]
        public void ThenCanResolveIDocumentationBuilderAsDitaDocumentationBuilderIfTheUserSelectsDitaOutput()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.DocumentationFormat = DocumentationFormat.Dita;

            var item = Kernel.Get<IDocumentationBuilder>();

            item.ShouldNotBeNull();
            item.ShouldBeType<DitaDocumentationBuilder>();
        }

        [Test]
        public void ThenCanResolveIDocumentationBuilderAsDitaDocumentationBuilderAsSingletonIfTheUserSelectsDitaOutput()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.DocumentationFormat = DocumentationFormat.Dita;

            var item1 = Kernel.Get<IDocumentationBuilder>();
            var item2 = Kernel.Get<IDocumentationBuilder>();

            item1.ShouldNotBeNull();
            item1.ShouldBeType<DitaDocumentationBuilder>();
            item2.ShouldNotBeNull();
            item2.ShouldBeType<DitaDocumentationBuilder>();
            item1.ShouldBeSameAs(item2);
        }

        [Test]
        public void ThenCanResolveIDocumentationBuilderAsExcelDocumentationBuilderIfTheUserSelectsExcelOutput()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.DocumentationFormat = DocumentationFormat.Excel;

            var item = Kernel.Get<IDocumentationBuilder>();

            item.ShouldNotBeNull();
            item.ShouldBeType<ExcelDocumentationBuilder>();
        }

        [Test]
        public void ThenCanResolveIDocumentationBuilderAsExcelDocumentationBuilderAsSingletonIfTheUserSelectsExcelOutput()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.DocumentationFormat = DocumentationFormat.Excel;

            var item1 = Kernel.Get<IDocumentationBuilder>();
            var item2 = Kernel.Get<IDocumentationBuilder>();

            item1.ShouldNotBeNull();
            item1.ShouldBeType<ExcelDocumentationBuilder>();
            item2.ShouldNotBeNull();
            item2.ShouldBeType<ExcelDocumentationBuilder>();
            item1.ShouldBeSameAs(item2);
        }
    }
}