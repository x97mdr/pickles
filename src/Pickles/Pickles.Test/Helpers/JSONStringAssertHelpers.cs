using System;
using NFluent;

namespace PicklesDoc.Pickles.Test.Helpers
{
    public static class JSONStringAssertHelpers
    {
        private const string KEY_VALUE_TEMPLATE = "\"{0}\": \"{1}\"";
        private const string ARRAY_TEMPLATE = "\"{0}\": [\r\n      \"{1}\"\r\n    ]";

        public static void AssertJSONKeyValue(this string json, string key, string value)
        {
            Check.That(json).Contains(string.Format(KEY_VALUE_TEMPLATE, key, value));
        }

        public static void AssertJSONArrayValue(this string json, string key, string value)
        {
            Check.That(json).Contains(string.Format(ARRAY_TEMPLATE, key, value));
        }

        public static void AssertJsonContainsKey(this string json, string key)
        {
            Check.That(json).Contains(key);
        }
    }
}