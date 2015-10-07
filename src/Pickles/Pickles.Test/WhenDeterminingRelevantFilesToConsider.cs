//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WhenDeterminingRelevantFilesToConsider.cs" company="PicklesDoc">
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
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.DirectoryCrawler;
using NFluent;

namespace PicklesDoc.Pickles.Test
{
    [TestFixture]
    public class WhenDeterminingRelevantFilesToConsider : BaseFixture
    {
        [Test]
        public void ThenCanDetectFeatureFilesSuccessfully()
        {
            var relevantFileDetector = Container.Resolve<RelevantFileDetector>();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.feature"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.markdown"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.mdown"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.mkdn"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.md"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.mdwn"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.mdtext"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.mdtxt"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.text"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.txt"))).IsTrue();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.pdf"))).IsFalse();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.doc"))).IsFalse();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.docx"))).IsFalse();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("test.docx"))).IsFalse();
            Check.That(relevantFileDetector.IsRelevant(FileSystem.FileInfo.FromFileName("myproject.csproj.FileListAbsolute.txt"))).IsFalse();
        }
    }
}