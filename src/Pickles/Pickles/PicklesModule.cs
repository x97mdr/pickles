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

using Ninject;
using Pickles.DocumentationBuilders.HTML;
using Pickles.DocumentationBuilders.Word;
using Pickles.TestFrameworks;

namespace Pickles
{
    public class PicklesModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<Configuration>().ToSelf().InSingletonScope();

            Bind<IDocumentationBuilder>().To<HtmlDocumentationBuilder>().When(request => Kernel.Get<Configuration>().DocumentationFormat == DocumentationFormat.Html).InSingletonScope();
            Bind<IDocumentationBuilder>().To<WordDocumentationBuilder>().When(request => Kernel.Get<Configuration>().DocumentationFormat == DocumentationFormat.Word).InSingletonScope();

            Bind<NUnitResults>().ToSelf().InSingletonScope();

            Bind<LanguageServices>().ToSelf().InSingletonScope();
            Bind<HtmlTableOfContentsFormatter>().ToSelf().InSingletonScope();
            Bind<HtmlFooterFormatter>().ToSelf().InSingletonScope();
            Bind<HtmlDocumentFormatter>().ToSelf().InSingletonScope();
            Bind<HtmlFeatureFormatter>().ToSelf().InSingletonScope();
            Bind<HtmlScenarioFormatter>().ToSelf().InSingletonScope();
            Bind<HtmlStepFormatter>().ToSelf().InSingletonScope();
            Bind<HtmlTableFormatter>().ToSelf().InSingletonScope();
            Bind<HtmlMultilineStringFormatter>().ToSelf().InSingletonScope();
            Bind<HtmlDescriptionFormatter>().ToSelf().InSingletonScope();
       }
    }
}
