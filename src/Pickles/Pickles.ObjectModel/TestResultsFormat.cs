//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TestResultsFormat.cs" company="PicklesDoc">
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

namespace PicklesDoc.Pickles
{
    /// <summary>
    /// The supported test result formats.
    /// </summary>
    public enum TestResultsFormat
    {
        /// <summary>
        /// NUnit 2 format.
        /// </summary>
        NUnit,

        /// <summary>
        /// xUnit 1 format.
        /// </summary>
        XUnit,

        /// <summary>
        /// xUnit 1 format.
        /// </summary>
        XUnit1,

        /// <summary>
        /// Microsoft Test format.
        /// </summary>
        MsTest,

        /// <summary>
        /// Cucumber's JSON format.
        /// </summary>
        CucumberJson,

        /// <summary>
        /// SpecRun format.
        /// </summary>
        SpecRun,

        /// <summary>
        /// xUnit 2 format.
        /// </summary>
        xUnit2,

        /// <summary>
        /// NUnit 3 format.
        /// </summary>
        NUnit3,

        /// <summary>
        /// The format produced by VsTest console.
        /// </summary>
        VsTest
    }
}
