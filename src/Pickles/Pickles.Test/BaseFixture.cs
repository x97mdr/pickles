using System;
using NUnit.Framework;
using Autofac;

namespace PicklesDoc.Pickles.Test
{
    public class BaseFixture
    {
        private IContainer container;

        protected IContainer Container
        {
            get 
            {
                if (this.container == null)
                {
                    var builder = new ContainerBuilder();
                    builder.RegisterAssemblyTypes(typeof (Runner).Assembly);
                    builder.RegisterModule<PicklesModule>();
                    this.container = builder.Build();
                }

                return this.container;
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (this.container != null)
                this.container.Dispose();
            this.container = null;
        }
    }
}