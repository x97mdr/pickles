using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Reflection;
using System.IO;

namespace Pickles.Test
{
    public class ParserFileFactory
    {
        public static IEnumerable<TestCaseData> Files
        {
            get
            {
                var resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();
                var features = resources.Where(name => name.EndsWith(".feature"));
                var xhtmls = resources.Where(name => name.EndsWith(".html"));

                foreach (var feature in features)
                {
                    var featureName = feature.Replace(".feature", string.Empty);
                    var associatedXhtml = xhtmls.SingleOrDefault(name => String.Equals(name.Replace(".html", string.Empty), featureName, StringComparison.InvariantCultureIgnoreCase));

                    if (associatedXhtml != null)
                    {
                        using (var featureStreamReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(feature)))
                        using (var xhtmlStreamReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(associatedXhtml)))
                        {
                            var featureText = featureStreamReader.ReadToEnd();
                            var associatedXhtmlText = xhtmlStreamReader.ReadToEnd();

                            yield return new TestCaseData(featureText, associatedXhtmlText);
                        }
                    }
                }
            }
        }
    }
}
