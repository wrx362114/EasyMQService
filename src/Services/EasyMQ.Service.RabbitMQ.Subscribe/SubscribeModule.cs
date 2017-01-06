using EasyMQ.Framework.Common.Extension;
using EasyNetQ;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMQ.Service.RabbitMQ.Subscribe
{
    public class SubscribeModule : NinjectModule
    {
        public override void Load()
        {
            Rebind<IBus>()
                .ToMethod(m => RabbitHutch.CreateBus("RabbitServerUrl".GetAppSetting()))
                .InSingletonScope();
        }
    }
}
