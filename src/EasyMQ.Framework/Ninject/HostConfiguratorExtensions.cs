 using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf.HostConfigurators;
using Topshelf.Logging;

namespace EasyMQ.Framework.Ninject
{
	public static class HostConfiguratorExtensions
	{
		public static HostConfigurator UseNinject(this HostConfigurator configurator)
		{
			return UseNinject(configurator, null);
		}
		public static HostConfigurator UseNinject(this HostConfigurator configurator, INinjectSettings settings)
		{
			var log = HostLogger.Get(typeof(HostConfiguratorExtensions));

			log.Info("[Topshelf.Ninject] 开始整合到宿主中");
			configurator.AddConfigurator(new NinjectBuilderConfigurator(settings));
			return configurator;
		}
	}
}
