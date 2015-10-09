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
    public struct TestResult
    {
        private readonly bool wasExecuted;

        private readonly bool wasSuccessful;

        private static readonly TestResult passed = new TestResult(wasExecuted: true, wasSuccessful: true);

        private static readonly TestResult failed = new TestResult(wasExecuted: true, wasSuccessful: false);

        private static readonly TestResult inconclusive = new TestResult(wasExecuted: false, wasSuccessful: false);

        private TestResult(bool wasExecuted, bool wasSuccessful)
        {
            this.wasExecuted = wasExecuted;
            this.wasSuccessful = wasSuccessful;
        }

        public bool WasExecuted
        {
            get { return this.wasExecuted; }
        }

        public bool WasSuccessful
        {
            get { return this.wasSuccessful; }
        }

        public static TestResult Passed
        {
            get { return passed; }
        }

        public static TestResult Failed
        {
            get { return failed; }
        }

        public static TestResult Inconclusive
        {
            get { return inconclusive; }
        }

        public bool Equals(TestResult other)
        {
            return this.WasExecuted.Equals(other.WasExecuted) && this.WasSuccessful.Equals(other.WasSuccessful);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
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
            return string.Format("WasExecuted: {0}, WasSuccessful: {1}", this.WasExecuted, this.WasSuccessful);
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