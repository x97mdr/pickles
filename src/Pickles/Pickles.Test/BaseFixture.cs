using NUnit.Framework;
using Ninject;

namespace Pickles.Test
{
    public class BaseFixture
    {
        private IKernel kernel;

        protected IKernel Kernel
        {
            get { return kernel ?? (kernel = new StandardKernel(new PicklesModule())); }
        }

        [TearDown]
        public void TearDown()
        {
            if (kernel != null)
                kernel.Dispose();
            kernel = null;
        }
    }
}