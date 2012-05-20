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

using System.Xml.Linq;
using Pickles.Parser;

namespace Pickles.DocumentationBuilders.DITA
{
    public class DitaStepFormatter
    {
        private readonly DitaTableFormatter ditaTableFormatter;

        public DitaStepFormatter(DitaTableFormatter ditaTableFormatter)
        {
            this.ditaTableFormatter = ditaTableFormatter;
        }

        public void Format(XElement section, Step step)
        {
            section.Add(new XElement("p", new XElement("keyword", step.NativeKeyword), step.Name));

            if (!string.IsNullOrEmpty(step.DocStringArgument))
            {
                section.Add(new XElement("pre", step.DocStringArgument));
            }

            if (step.TableArgument != null)
            {
                ditaTableFormatter.Format(section, step.TableArgument);
            }
        }
    }
}