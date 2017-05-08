using Ninject;
using Ninject.Modules;
using Topshelf.Logging;
using ES.Framework.Core; 
using Quartz.Impl;
using ES.Framework.Quartz;
using Quartz;

namespace ES.Framework.Ninject
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            Rebind<Logger>()
                .ToMethod(m => new Logger(HostLogger.Get("Default")))
                .InSingletonScope();
            Rebind<IKernel>()
                .ToMethod(m => NinjectBuilderConfigurator.Kernel);
            Rebind<IScheduler>()
                .ToMethod(m =>
                {
                    var s = StdSchedulerFactory.GetDefaultScheduler();
                    s.JobFactory = new NinjectJobFactory(NinjectBuilderConfigurator.Kernel);
                    return s;
                })
                .InSingletonScope();

        }
    }
}
