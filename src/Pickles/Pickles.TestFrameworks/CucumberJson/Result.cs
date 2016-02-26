//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Result.cs" company="PicklesDoc">
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
using System.Diagnostics.CodeAnalysis;

namespace PicklesDoc.Pickles.TestFrameworks.CucumberJson
{
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "The lowercase name is part of an external contract.")]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "The lowercase name is part of an external contract.")]
    public class Result
    {
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "The lowercase name is part of an external contract.")]
        public string status { get; set; }

        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "The lowercase name is part of an external contract.")]
        public string error_message { get; set; }
    }
}
