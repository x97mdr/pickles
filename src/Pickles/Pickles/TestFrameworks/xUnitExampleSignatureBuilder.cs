using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Pickles.Parser;

namespace Pickles.TestFrameworks
{
    public class xUnitExampleSignatureBuilder
    {
        public Regex Build(ScenarioOutline scenarioOutline, string[] row)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(scenarioOutline.Name.ToLowerInvariant().Replace(" ", string.Empty) + "\\(");

            foreach (string value in row)
            {
                stringBuilder.AppendFormat("(.*): \"{0}\", ", value);
            }

            stringBuilder.Remove(stringBuilder.Length - 2, 2);

            return new Regex(stringBuilder.ToString());
        }
    }
}
