using EasyNetQ;
using Ninject.Modules;

namespace ES.Framework.RabbitMQ
{
    public class RabbitMQModule : NinjectModule
    {
        public override void Load()
        {//TODO:将队列功能集中到一个服务中.而不是现在的直接提供总线操作.
            Rebind<IBus>()
                .ToMethod(m => Common.Extend.IEventMsgExtend.Bus)
                .InSingletonScope();
        }
    }
}
