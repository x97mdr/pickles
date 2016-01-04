//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TestResult.cs" company="PicklesDoc">
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
using System.Collections.Generic;
using System.Linq;

namespace PicklesDoc.Pickles.TestFrameworks
{
    public enum TestResult
    {
        Inconclusive,
        Failed,
        Passed
    }
    /*
    public struct TestResult
    {
        private TestResult(bool wasExecuted, bool wasSuccessful)
        {
            this.WasExecuted = wasExecuted;
            this.WasSuccessful = wasSuccessful;
        }

        public static TestResult Passed { get; } = new TestResult(wasExecuted: true, wasSuccessful: true);

        public static TestResult Failed { get; } = new TestResult(wasExecuted: true, wasSuccessful: false);

        public static TestResult Inconclusive { get; } = new TestResult(wasExecuted: false, wasSuccessful: false);

        public bool WasExecuted { get; }

        public bool WasSuccessful { get; }

        public bool Equals(TestResult other)
        {
            return this.WasExecuted.Equals(other.WasExecuted) && this.WasSuccessful.Equals(other.WasSuccessful);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is TestResult && Equals((TestResult)obj);
        }

        public override int GetHashCode()
        {
            int hashCode = this.WasExecuted.GetHashCode();
            hashCode = hashCode ^ this.WasSuccessful.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"WasExecuted: {this.WasExecuted}, WasSuccessful: {this.WasSuccessful}";
        }

        public static bool operator ==(TestResult left, TestResult right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TestResult left, TestResult right)
        {
            return !(left == right);
        }
    }
    */

    public static class TestResultExtensions
    {
        public static TestResult Merge(this IEnumerable<TestResult> testResults)
        {
            if (testResults == null)
            {
                throw new ArgumentNullException("testResults");
            }

            TestResult[] items = testResults.ToArray();

            if (!items.Any())
            {
                return new TestResult();
            }

            if (items.Count() == 1)
            {
                return items.Single();
            }

            if (items.Any(i => i == TestResult.Failed))
            {
                return TestResult.Failed;
            }

            if (items.Any(i => i == TestResult.Inconclusive))
            {
                return TestResult.Inconclusive;
            }

            return TestResult.Passed;
        }
    }
}
