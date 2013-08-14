using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PicklesDoc.Pickles.Parser.SpecRun
{
    internal static class Factory
    {
        /*
                <features>
                    <feature>
                        <title>Addition</title>
                        <scenarios>
                            <scenario>
                                <title>Adding several numbers</title>
                                <result>Passed|Pending|Failed|Ignored</result>
                            </scenario>
                        </scenarios>
                    </feature>
                </features>
           */

        internal static Feature ToSpecRunFeature(XElement featureXml)
        {
            var title = featureXml.Element("title");
            var scenarios = featureXml.Element("scenarios");

            return new Feature
            {
                Title = title != null ? title.Value : string.Empty,
                Scenarios = scenarios != null ? scenarios.Elements("scenario").Select<XElement, Scenario>(ToSpecRunScenario).ToList() : new List<Scenario>()
            };
        }

        internal static Scenario ToSpecRunScenario(XElement scenarioXml)
        {
            var title = scenarioXml.Element("title");
            var result = scenarioXml.Element("result");

            return new Scenario
            {
                Title = title != null ? title.Value : string.Empty,
                Result = result != null ? result.Value : string.Empty
            };
        }
    }
}