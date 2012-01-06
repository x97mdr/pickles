using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ninject;
using NUnit.Framework;
using Pickles.Parser;
using Should.Fluent;

namespace Pickles.Test.Formatters.JSON
{
    [TestFixture]
    public class WhenFormattingASingleFeatureToJSON : BaseFixture
    {
        private JSONFeatureFormatter _jsonFeatureFormatter;
        private Feature _feature;
        private string _featureJSON;


        [TestFixtureSetUp]
        public void Setup()
        {
            var parser = Kernel.Get<FeatureParser>();
            _feature =
                parser.Parse(
                    @"C:\Dev\pickles\src\Pickles\Pickles.Test\FakeFolderStructures\AcceptanceTest\AdvancedFeature.feature");

            _jsonFeatureFormatter = new JSONFeatureFormatter();
            _featureJSON = _jsonFeatureFormatter.Format(_feature);
            Debug.WriteLine(_featureJSON);
        }

        private void AssertJSONKeyValue(string key, string value)
        {
            var template = "\"{0}\": \"{1}\"";
            _featureJSON.Should().Contain(string.Format(template, key, value));
        }

        private void AssertJSONArrayValue(string key, string value)
        {
            var template = "\"{0}\": [\r\n    \"{1}\"\r\n  ]";
            _featureJSON.Should().Contain(string.Format(template, key, value));
        }

        [Test]
        public void it_should_contain_the_feature_name()
        {
            // Assert
            AssertJSONKeyValue("Name", "Feature title");
        }

        [Test]
        public void it_should_contain_the_feature_tag()
        {
            // Assert
            AssertJSONArrayValue("Tags", "@FeatureTag");
        }

        [Test]
        public void it_should_format_keywords_to_correct_string()
        {
            AssertJSONKeyValue("Keyword", "Given");
            AssertJSONKeyValue("Keyword", "When");
            AssertJSONKeyValue("Keyword", "Then");
            AssertJSONKeyValue("Keyword", "And");
            AssertJSONKeyValue("Keyword", "But");
        }

        [Test]
        public void it_should_format_native_keywords()
        {
            AssertJSONKeyValue("NativeKeyword", "Given ");
            AssertJSONKeyValue("NativeKeyword", "When ");
            AssertJSONKeyValue("NativeKeyword", "Then ");
            AssertJSONKeyValue("NativeKeyword", "And ");
            AssertJSONKeyValue("NativeKeyword", "But ");
        }

        [Test]
        public void it_should_contain_a_background()
        {
            _featureJSON.Should().Contain("\"Background\"");
            AssertJSONKeyValue("Name", "Background name");
        }
    }

    public class JSONFeatureFormatter
    {
        private JsonSerializerSettings _jsonSerializerSettings;

        public JSONFeatureFormatter()
        {
            _jsonSerializerSettings = new JsonSerializerSettings
                                          {
                                              ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                              NullValueHandling = NullValueHandling.Ignore,
                                              Converters = new List<JsonConverter>
                                                               {
                                                                   new StringEnumConverter()
                                                               }
                                          };
        }

        public string Format(Feature feature)
        {
            return JsonConvert.SerializeObject(feature, Formatting.Indented, _jsonSerializerSettings);
        }
    }
}
