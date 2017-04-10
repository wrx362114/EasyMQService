using System;
using ES.Framework.Core;
using ES.Model.Message;
using ES.Model;

namespace ES.Framework.RabbitMQ
{
    public abstract class BaseEvent<T> : IEvent where T : class, IEventMsg
    {
        public Logger Log { get; set; }
        public BaseEvent(Logger log)
        {
            Log = log;
        }
        public abstract EventType Event { get; }

        public ResultModel Handler(IEventMsg msg)
        {
            if (msg is T)
            {
                return Handler(msg as T);
            }
            return ResultModel.Failed("事件消息类型错误");
        }

        public abstract ResultModel Handler(T msg);

        public virtual void ErrorHandler(T msg, Exception ex)
        {
            Log.Error("执行任务时触发异常:"+ex.ToString());
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
