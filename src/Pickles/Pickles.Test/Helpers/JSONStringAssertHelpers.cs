using System;
using Should.Fluent;

namespace PicklesDoc.Pickles.Test.Helpers
{
    public static class JSONStringAssertHelpers
    {
        private const string KEY_VALUE_TEMPLATE = "\"{0}\": \"{1}\"";
        private const string ARRAY_TEMPLATE = "\"{0}\": [\r\n      \"{1}\"\r\n    ]";

        public static void AssertJSONKeyValue(this string json, string key, string value)
        {
            json.Should().Contain(string.Format(KEY_VALUE_TEMPLATE, key, value));
        }

        public static void AssertJSONArrayValue(this string json, string key, string value)
        {
            json.Should().Contain(string.Format(ARRAY_TEMPLATE, key, value));
        }

        public static void AssertJsonContainsKey(this string json, string key)
        {
            json.Should().Contain(key);
        }
    }
}