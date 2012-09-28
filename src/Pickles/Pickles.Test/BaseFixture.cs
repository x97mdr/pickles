using NUnit.Framework;
using Autofac;

namespace Pickles.Test
{
    public class BaseFixture
    {
        private IContainer container;

        protected IContainer Container
        {
            get 
            {
                if (container == null)
                {
                    var builder = new ContainerBuilder();
                    builder.RegisterAssemblyTypes(typeof (Runner).Assembly);
                    builder.RegisterModule<PicklesModule>();
                    container = builder.Build();
                }

                return container;
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (container != null)
                container.Dispose();
            container = null;
        }
    }
}