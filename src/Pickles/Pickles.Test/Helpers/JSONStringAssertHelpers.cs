//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="JsonStringAssertHelpers.cs" company="PicklesDoc">
//  Copyright 2011 Jeffrey Cameron
//  Copyright 2012-present PicklesDoc team and community contributors
//
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using NFluent;

namespace PicklesDoc.Pickles.Test.Helpers
{
    public static class JsonStringAssertHelpers
    {
        private const string KeyValueTemplate = "\"{0}\": \"{1}\"";
        private const string ArrayTemplate = "\"{0}\": [\r\n      \"{1}\"\r\n    ]";

        public static void AssertJsonKeyValue(this string json, string key, string value)
        {
            Check.That(json).Contains(string.Format(KeyValueTemplate, key, value));
        }

        public static void AssertJsonArrayValue(this string json, string key, string value)
        {
            Check.That(json).Contains(string.Format(ArrayTemplate, key, value));
        }

        public static void AssertJsonContainsKey(this string json, string key)
        {
            Check.That(json).Contains(key);
        }
    }
}