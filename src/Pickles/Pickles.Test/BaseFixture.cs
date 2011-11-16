using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace Pickles.Test
{
    using NUnit.Framework;

    public class BaseFixture
    {
        private IKernel kernel;

        protected IKernel Kernel
        {
            get { return kernel; }
        }

        [SetUp]
        public void SetUp()
        {
            this.kernel = new StandardKernel(new PicklesModule());
        }

        [TearDown]
        public void TearDown()
        {
            this.kernel.Dispose();
        }
    }
}
