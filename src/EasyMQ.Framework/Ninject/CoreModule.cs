using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMQ.Framework.Ninject
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            Rebind<log4net.ILog>()
                .ToMethod(m => log4net.LogManager.GetLogger("Default"))
                .InSingletonScope();
            Rebind<IKernel>()
                .ToMethod(m => NinjectBuilderConfigurator.Kernel);

        }
    }
}
