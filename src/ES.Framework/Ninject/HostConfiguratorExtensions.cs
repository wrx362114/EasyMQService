using Ninject;
using Topshelf.HostConfigurators;
using Topshelf.Logging;

namespace ES.Framework.Ninject
{
    public static class HostConfiguratorExtensions
    {
        public static HostConfigurator UseNinject(this HostConfigurator configurator)
        {
            return UseNinject(configurator, null);
        }
        public static HostConfigurator UseNinject(this HostConfigurator configurator, INinjectSettings settings)
        {
            var log = HostLogger.Get("Default");

            log.Info("[Topshelf.Ninject] 开始整合到宿主中");
            configurator.AddConfigurator(new NinjectBuilderConfigurator(settings));
            return configurator;
        }
    }
}
