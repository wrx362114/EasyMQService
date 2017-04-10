using System.Collections.Generic;
using System.Linq;
using Topshelf;

namespace ES.Framework.Core
{
    public class CoreService : ServiceControl
    {
        private IEnumerable<IService> Servicers;
        private Logger Log;
        public CoreService(IEnumerable<IService> services, Logger log)
        {
            Servicers = services;
            Log = log;
        }
        public bool Start(HostControl hostControl)
        {
            Log.Info("[CoreService] 正在启动核心服务");
            Servicers.AsParallel().ForAll(a => a.Start());
            Log.Info("[CoreService] 核心服务已启动");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            foreach (var item in Servicers)
            {
                item.Stop();
            }
            return true;
        }
    }
}
