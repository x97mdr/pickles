using System;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using Autofac;
using NUnit.Framework;

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
                    builder.RegisterAssemblyTypes(typeof(Runner).Assembly);
                    builder.Register<FileSystem>(_ => new FileSystem()).As<IFileSystem>().SingleInstance();
                    builder.Register<MockFileSystem>(_ => new MockFileSystem()).SingleInstance();
                    builder.RegisterModule<PicklesModule>();
                    this.container = builder.Build();
                }

                return this.container;
            }
        }

        protected IFileSystem RealFileSystem
        {
            get { return this.Container.Resolve<IFileSystem>(); }
        }

        protected MockFileSystem MockFileSystem
        {
            get { return this.Container.Resolve<MockFileSystem>(); }
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