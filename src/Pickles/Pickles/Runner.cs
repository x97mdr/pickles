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

namespace Pickles
{
    public class Runner
    {
        public void Run(IKernel kernel)
        {
            var configuration = kernel.Get<Configuration>();
            if (!configuration.OutputFolder.Exists) configuration.OutputFolder.Create();

            var documentationBuilder = kernel.Get<HtmlDocumentationBuilder>();
            documentationBuilder.Build(configuration.FeatureFolder, configuration.OutputFolder);
        }
    }
}
