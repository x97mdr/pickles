using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace Pickles.Test
{
    public class BaseFixture
    {
        public IKernel Kernel = new StandardKernel(new PicklesModule());
    }
}
