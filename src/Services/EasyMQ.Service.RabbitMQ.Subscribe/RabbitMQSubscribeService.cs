using EasyMQ.Framework.Common.Extension;
using EasyMQ.Framework.Core;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMQ.Service.RabbitMQ.Subscribe
{
    public class RabbitMQSubscribeService : IService
    {
        private IEnumerable<IEvent> Events { get; set; }
        private IBus Bus { get; set; }
        private log4net.ILog Log { get; set; }
        private ISubscriptionResult Subscription { get; set; }
        public RabbitMQSubscribeService(IBus bus,
                IEnumerable<IEvent> events,
                log4net.ILog log)
        {
            Bus = bus;
            Log = log;
            Events = events;
        }
        public bool Start()
        {
            if ("SubscribeEventBus".GetAppSetting() == "Enable")
            {
                var service = Bus.SubscribeAsync<IEventMsg>("EventBusService",
                   msg =>
                   {
                       Log.Info("处理事件:" + msg.ToJson());
                       return Task.Factory.StartNew(() =>
                       {
                           EventHander(msg);
                       });
                   });
                Log.Info("已关注事件总线");
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
                .Where(m => msg.Event == m.Event)
                .AsParallel()
                .ForEach(
                   m =>
                   {
                       try
                       {
                           var result = m.Handler(msg);
                           if (!result.IsCompleted)
                           {
                               Log.Warn(result.Message);
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
