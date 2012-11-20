#region License
/*
     Copyright [2011] [Jeffrey Cameron]
    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at
        http://www.apache.org/licenses/LICENSE-2.0
    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
 */
#endregion

using System;
using NUnit.Framework;
using Pickles.TestFrameworks;
using Should;

namespace Pickles.Test
{
    [TestFixture]
    public class TestResultExtensionsTests
    {
      [Test]
      [ExpectedException(typeof(ArgumentNullException))]
      public void Merge_NullReference_ThrowsArgumentNullException()
      {
        TestResultExtensions.Merge(null);
      }

      [Test]
      [ExpectedException(typeof(ArgumentException))]
      public void Merge_EmptySequence_ThrowsArgumentException()
      {
        var testResults = new TestResult[0];

        testResults.Merge();
      }

      [Test]
      public void Merge_SingleItem_ReturnsThatItem()
      {
        var testResults = new[]
          {
            new TestResult { WasExecuted = true, WasSuccessful = false }
          };

        TestResult actual = testResults.Merge();

        actual.WasExecuted.ShouldBeTrue();
        actual.WasSuccessful.ShouldBeFalse();
      }

      [Test]
      public void Merge_MultipleItemsWasExecutedTrueAndTrue_ReturnsWasExecutedTrue()
      {
        var testResults = new[]
          {
            new TestResult { WasExecuted = true }, new TestResult { WasExecuted = true }
          };

        TestResult actual = testResults.Merge();

        actual.WasExecuted.ShouldBeTrue();
      }

      [Test]
      public void Merge_MultipleItemsWasExecutedTrueAndFalse_ReturnsWasExecutedFalse()
      {
        var testResults = new[]
          {
            new TestResult { WasExecuted = true },
            new TestResult { WasExecuted = false }
          };

        TestResult actual = testResults.Merge();

        actual.WasExecuted.ShouldBeFalse();
      }

      [Test]
      public void Merge_MultipleItemsWasSuccessfulTrueAndTrue_ReturnsWasSuccessfulTrue()
      {
        var testResults = new[]
          {
            new TestResult { WasSuccessful = true },
            new TestResult { WasSuccessful = true }
          };

        TestResult actual = testResults.Merge();

        actual.WasSuccessful.ShouldBeTrue();
      }

      [Test]
      public void Merge_MultipleItemsWasSuccessfulTrueAndFalse_ReturnsWasSuccessfulFalse()
      {
        var testResults = new[]
          {
            new TestResult { WasSuccessful = true },
            new TestResult { WasSuccessful = false }
          };

        TestResult actual = testResults.Merge();

        actual.WasSuccessful.ShouldBeFalse();
      }
    }
}
