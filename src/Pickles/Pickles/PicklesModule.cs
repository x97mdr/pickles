using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pickles.Formatters;

namespace Pickles
{
    public class PicklesModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IDocumentationBuilder>().To<HtmlDocumentationBuilder>().InSingletonScope();

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
