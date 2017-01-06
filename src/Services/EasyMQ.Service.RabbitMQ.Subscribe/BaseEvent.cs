using EasyMQ.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMQ.Service.RabbitMQ.Subscribe
{
    public abstract class BaseEvent<T> : IEvent where T : class,IEventMsg
    {
        public abstract string Event { get; }

        public CommandResult Handler(IEventMsg msg)
        {
            if (msg is T)
            {
                return Handler(msg as T);
            }
            return CommandResult.Failed("事件消息类型错误");
        }

        public abstract CommandResult Handler(T msg);

        public virtual void ErrorHandler(T msg, Exception ex)
        {
            if (msg.RetryCount < 3)
            {
                msg.RetryCount++;
                msg.PublishAsync();
            }
            else
            {
                throw ex;
            }
        }

        public void ErrorHandler(IEventMsg msg, Exception ex)
        {
            if (msg is T)
            {
                ErrorHandler(msg as T, ex);
            }
        }
    }
}
