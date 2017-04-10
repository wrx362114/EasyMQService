using EasyNetQ;
using Ninject.Modules;

namespace ES.Framework.RabbitMQ
{
    public class RabbitMQModule : NinjectModule
    {
        public override void Load()
        {
            Rebind<IBus>()
                .ToMethod(m => Common.Extend.IEventMsgExtend.RabbitServerBus)
                .InSingletonScope();
        }
    }
}
