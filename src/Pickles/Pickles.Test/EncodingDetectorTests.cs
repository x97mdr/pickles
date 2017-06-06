//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EncodingDetectorTests.cs" company="PicklesDoc">
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
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using NFluent;
using NUnit.Framework;

namespace PicklesDoc.Pickles.Test
{
    [TestFixture]
    public class EncodingDetectorTests : BaseFixture
    {
        [Test]
        public void GetEncoding_NullArgument_ThrowsArgumentNullException()
        {
            var detector = this.CreateDetector();

            Check.ThatCode(() => detector.GetEncoding(null)).Throws<ArgumentNullException>();
        }

        private EncodingDetector CreateDetector()
        {
            return new EncodingDetector(FileSystem);
        }

        [Test]
        public void GetEncoding_FileDoesNotExist_DoesNotThrowException()
        {
            var detector = this.CreateDetector();

            Check.ThatCode(() => detector.GetEncoding("file-does-not-exist.feature")).DoesNotThrow();
        }

        [Test]
        public void GetEncoding_FileIsLessThanFourBytes_DoesNotThrowException()
        {
            FileSystem.AddFile("temp.feature", new MockFileData(new byte[3]));

            var detector = this.CreateDetector();

            Check.ThatCode(() => detector.GetEncoding("temp.feature")).DoesNotThrow();
        }

        [Test]
        public void GetEncoding_UTF7BOM_ReturnsUTF()
        {
            FileSystem.AddFile("utf7.feature", new MockFileData(new byte[] { 0x2b, 0x2f, 0x76 }));

            var detector = this.CreateDetector();

            var encoding = detector.GetEncoding("utf7.feature");

            Check.That(encoding).IsEqualTo(Encoding.UTF7);
        }

        [Test]
        public void GetEncoding_UTF16LEBOM_ReturnsUnicode()
        {
            FileSystem.AddFile("utf16le.feature", new MockFileData(new byte[] { 0xff, 0xfe }));

            var detector = this.CreateDetector();

            var encoding = detector.GetEncoding("utf16le.feature");

            Check.That(encoding).IsEqualTo(Encoding.Unicode);
        }

        [Test]
        public void GetEncoding_UTF16BEBOM_ReturnsUnicode()
        {
            FileSystem.AddFile("utf16be.feature", new MockFileData(new byte[] { 0xfe, 0xff }));

            var detector = this.CreateDetector();

            var encoding = detector.GetEncoding("utf16be.feature");

            Check.That(encoding).IsEqualTo(Encoding.BigEndianUnicode);
        }

        [Test]
        public void GetEncoding_UTF32BOM_ReturnsUnicode()
        {
            FileSystem.AddFile("utf32.feature", new MockFileData(new byte[] { 0, 0, 0xfe, 0xff }));

            var detector = this.CreateDetector();

            var encoding = detector.GetEncoding("utf32.feature");

            Check.That(encoding).IsEqualTo(Encoding.UTF32);
        }

        [Test]
        public void GetEncoding_Default_ReturnsUTF8()
        {
            FileSystem.AddFile("other.feature", new MockFileData(new byte[] { 0xab, 0xcd, 0xef, 0x01 }));

            var detector = this.CreateDetector();

            var encoding = detector.GetEncoding("other.feature");

            Check.That(encoding).IsEqualTo(Encoding.UTF8);
        }
    }
}