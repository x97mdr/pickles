﻿using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Specs.ContextInjection
{
    [Binding]
    public class FeatureWithARecursiveContextSteps
    {
        private readonly NestedContext _context;

        public FeatureWithARecursiveContextSteps(NestedContext context)
        {
            _context = context;
        }

        [Given("a feature which requires a recursive context")]
        public void GivenAFeatureWhichRequiresARecursiveContext()
        {
        }

        [Then("its sub-context is set")]
        public void ThenItsSubContextIsSet()
        {
            Assert.That(_context.TheNestedContext, Is.Not.Null);
        }
    }
}
