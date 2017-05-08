using ES.Framework.Core;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using ES.Common.Extend;
using ES.Model.Message;

namespace ES.Framework.RabbitMQ
{
    public class SubscribeService : IService
    {
        private IEnumerable<IEvent> Events { get; set; }
        private IBus Bus { get; set; }
        private Logger Log { get; set; }
        private ISubscriptionResult Subscription { get; set; }

        public string Name => "消息订阅服务";

        public SubscribeService(IBus bus,
                IEnumerable<IEvent> events,
                Logger log)
        {
            Bus = bus;
            Log = log;
            Events = events;
        }
        public bool Start()
        {
            Log.Info("[RabbitMQService] 正在启动事件处理服务 ,共注册了 [" + Events.Count() + " ]个事件");
            if ("SubscribeEventBus".GetConfig() == "Enable")
            {
                var service = Bus.SubscribeAsync<IEventMsg>("EventBusService",
                   msg =>
                   {
                       Log.Info($"EventBus 正在处理事件:" + msg.Event.ToString() + msg.ToJson());
                       return System.Threading.Tasks.Task.Factory.StartNew(() =>
                       {
                           EventHander(msg);
                       });
                   });

                Log.Info("[RabbitMQService] 已关注事件总线");
            }
            else
            {
                Log.Info("[RabbitMQService] 未启用事件处理服务");
            }
            return true;
        }

        public bool Stop()
        {
            if (Subscription != null)
            {
                Subscription.SafeDispose();
            }
            return true;
        }
        private void EventHander(IEventMsg msg)
        {
            //将事件消息分发到事件总线var eventMsg = msg as EventMsgModel;
            Events
                .Where(m => msg.Event == m.Event)//TODO:直接使用泛型参数分发消息.不使用枚举.能节约一个定义枚举的操作.
               // .AsParallel()
               .ToList()
               .ForEach(
                   m =>
                   {
                       try
                       {
                           var result = m.Handler(msg);
                           if (!result.Succeed)
                           {
                               Log.Warn(result.Msg);
                           }
                       }
                       catch (Exception ex)
                       {
                           m.ErrorHandler(msg, ex);
                       }
                   });
        }

    }
}
