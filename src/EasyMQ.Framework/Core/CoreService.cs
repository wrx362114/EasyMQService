using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace EasyMQ.Framework.Core
{
    public class CoreService : ServiceControl
    {
        private IEnumerable<IService> Servicers;
        public CoreService(IEnumerable<IService> services)
        {
            Servicers = services;
        }
        public bool Start(HostControl hostControl)
        {
            foreach (var item in Servicers)
            {
                item.Start();
            }
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
