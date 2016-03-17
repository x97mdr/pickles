//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WhenResolvingAMarkdownProvider.cs" company="PicklesDoc">
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

using Autofac;
using Autofac.Core;

using NFluent;

using NUnit.Framework;

namespace PicklesDoc.Pickles.Test
{
    [TestFixture]
    public class WhenResolvingAMarkdownProvider : BaseFixture
    {
        [Test]
        public void WhenExperimentalIsEnabled_ResolveStrikeMarkdownProvider()
        {
            var configuration = this.Configuration;

            configuration.EnableExperimentalFeatures();

            var markdownProvider = Container.Resolve<IMarkdownProvider>();

            Check.That(markdownProvider).IsInstanceOf<StrikeMarkdownProvider>();
        }

        [Test]
        public void WhenExperimentalIsDisabled_ResolveMarkdownProvider()
        {
            var configuration = this.Configuration;

            configuration.DisableExperimentalFeatures();

            var markdownProvider = Container.Resolve<IMarkdownProvider>();

            Check.That(markdownProvider).IsInstanceOf<MarkdownProvider>();
        }
    }
}