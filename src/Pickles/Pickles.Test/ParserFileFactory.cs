using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using NUnit.Framework;

namespace PicklesDoc.Pickles.Test
{
    public class ParserFileFactory
    {
        public static IEnumerable<TestCaseData> Files
        {
            get
            {
                string[] resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();
                IEnumerable<string> features = resources.Where(name => name.EndsWith(".feature"));
                IEnumerable<string> xhtmls = resources.Where(name => name.EndsWith(".html"));

                foreach (string feature in features)
                {
                    string featureName = feature.Replace(".feature", string.Empty);
                    string associatedXhtml =
                        xhtmls.SingleOrDefault(
                            name =>
                            String.Equals(name.Replace(".html", string.Empty), featureName,
                                          StringComparison.InvariantCultureIgnoreCase));

                    if (associatedXhtml != null)
                    {
                        using (
                            var featureStreamReader =
                                new System.IO.StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(feature)))
                        using (
                            var xhtmlStreamReader =
                                new System.IO.StreamReader(
                                    Assembly.GetExecutingAssembly().GetManifestResourceStream(associatedXhtml)))
                        {
                            string featureText = featureStreamReader.ReadToEnd();
                            string associatedXhtmlText = xhtmlStreamReader.ReadToEnd();

                            yield return new TestCaseData(featureText, associatedXhtmlText);
                        }
                    }
                }
            }
        }
    }
}