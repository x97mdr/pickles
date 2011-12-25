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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using NUnit.Framework;
using Pickles.DocumentationBuilders.Word;
using Pickles.DocumentationBuilders.DITA;

namespace Pickles.Test
{
    public class WhenResolvingADocumentationBuilder : BaseFixture
    {
        public void Then_can_resolve_IDocumentationBuilder_as_HtmlDocumentationBuilder_if_the_user_selects_HTML_output()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.DocumentationFormat = DocumentationFormat.Html;

            var item = Kernel.Get<IDocumentationBuilder>();

            Assert.NotNull(item);
            Assert.IsInstanceOf<HtmlDocumentationBuilder>(item);
        }

        public void Then_can_resolve_IDocumentationBuilder_as_HtmlDocumentationBuilder_as_singleton_if_the_user_selects_HTML_output()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.DocumentationFormat = DocumentationFormat.Html;

            var item1 = Kernel.Get<IDocumentationBuilder>();
            var item2 = Kernel.Get<IDocumentationBuilder>();

            Assert.NotNull(item1);
            Assert.IsInstanceOf<HtmlDocumentationBuilder>(item1);
            Assert.NotNull(item2);
            Assert.IsInstanceOf<HtmlDocumentationBuilder>(item2);
            Assert.AreSame(item1, item2);
        }

        public void Then_can_resolve_IDocumentationBuilder_as_WordDocumentationBuilder_if_the_user_selects_Word_output()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.DocumentationFormat = DocumentationFormat.Word;

            var item = Kernel.Get<IDocumentationBuilder>();

            Assert.NotNull(item);
            Assert.IsInstanceOf<WordDocumentationBuilder>(item);
        }

        public void Then_can_resolve_IDocumentationBuilder_as_WordDocumentationBuilder_as_singleton_if_the_user_selects_Word_output()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.DocumentationFormat = DocumentationFormat.Word;

            var item1 = Kernel.Get<IDocumentationBuilder>();
            var item2 = Kernel.Get<IDocumentationBuilder>();

            Assert.NotNull(item1);
            Assert.IsInstanceOf<WordDocumentationBuilder>(item1);
            Assert.NotNull(item2);
            Assert.IsInstanceOf<WordDocumentationBuilder>(item2);
            Assert.AreSame(item1, item2);
        }

        public void Then_can_resolve_IDocumentationBuilder_as_DitaDocumentationBuilder_if_the_user_selects_DITA_output()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.DocumentationFormat = DocumentationFormat.Dita;

            var item = Kernel.Get<IDocumentationBuilder>();

            Assert.NotNull(item);
            Assert.IsInstanceOf<DitaDocumentationBuilder>(item);
        }

        public void Then_can_resolve_IDocumentationBuilder_as_DitaDocumentationBuilder_as_singleton_if_the_user_selects_DITA_output()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.DocumentationFormat = DocumentationFormat.Dita;

            var item1 = Kernel.Get<IDocumentationBuilder>();
            var item2 = Kernel.Get<IDocumentationBuilder>();

            Assert.NotNull(item1);
            Assert.IsInstanceOf<DitaDocumentationBuilder>(item1);
            Assert.NotNull(item2);
            Assert.IsInstanceOf<DitaDocumentationBuilder>(item2);
            Assert.AreSame(item1, item2);
        }
    }
}
