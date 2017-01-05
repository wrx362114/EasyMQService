using Ninject;
using Ninject.Modules;
using System;
using System.Linq;
using Topshelf;
using Topshelf.HostConfigurators;
using Topshelf.Logging;
using Topshelf.ServiceConfigurators;

namespace EasyMQ.Framework.Ninject
{
	public static class ServiceConfiguratorExtensions
	{
		public static ServiceConfigurator<T> ConstructUsingNinject<T>(this ServiceConfigurator<T> configurator) where T : class
		{
			var log = HostLogger.Get(typeof(HostConfiguratorExtensions));

			log.Info("[Topshelf.Ninject] 核心服务已配置为使用Ninject构造 .");

			configurator.ConstructUsing(serviceFactory => NinjectBuilderConfigurator.Kernel.Get<T>());

			return configurator;
		}

	}
}
