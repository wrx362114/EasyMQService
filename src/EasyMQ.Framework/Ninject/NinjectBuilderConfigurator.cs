using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf.Builders;
using Topshelf.Configurators;
using Topshelf.HostConfigurators;
using Ninject.Extensions.Conventions;
using EasyMQ.Framework.Core;

namespace EasyMQ.Framework.Ninject
{
    public class NinjectBuilderConfigurator : HostBuilderConfigurator
    {
        public readonly static IKernel Kernel;
        static NinjectBuilderConfigurator()
        {
            Kernel = CreateKernel();
        }
        private static INinjectSettings _settings;
        private static IKernel CreateKernel()
        {
            var kernel = _settings == null ? new StandardKernel() : new StandardKernel(_settings);
            kernel.Bind(m =>
            {
                //加载插件dll,中的所有继承自单例接口的类为单例注入
                var b =
                m.FromAssembliesInPath("Plugins")
                    .Select(a => 
                        a.IsClass && a.GetInterface(typeof(ISingleton).FullName) != null);
                    b.BindAllInterfaces()
                    .Configure(c => c.InSingletonScope());
                m.FromThisAssembly()
                    .SelectAllClasses()
                    .BindAllInterfaces()
                    .Configure(c => c.InSingletonScope());
            });
            kernel.Load(kernel.GetAll<INinjectModule>());
            return kernel;
        }
        public NinjectBuilderConfigurator(INinjectSettings settings)
        {
            _settings = settings;
        }

        public IEnumerable<ValidateResult> Validate()
        {
            yield break;
        }

        public HostBuilder Configure(HostBuilder builder)
        {
            return builder;
        }
    }
}
