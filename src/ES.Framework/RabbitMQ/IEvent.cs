using ES.Framework.Core;
using System;
using ES.Model;
using ES.Model.Message;

namespace ES.Framework.RabbitMQ
{
    /// <summary> 事件处理基础接口 </summary>
    public interface IEvent : ISingleton
    {
        EventType Event { get; }
        ResultModel Handler(IEventMsg msg);
        void ErrorHandler(IEventMsg msg, Exception ex);
    }
}
