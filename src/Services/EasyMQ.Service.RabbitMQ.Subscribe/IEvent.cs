using EasyMQ.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMQ.Service.RabbitMQ.Subscribe
{
    /// <summary> 事件处理基础接口 </summary>
    public interface IEvent : ISingleton
    {
        string Event { get; }
        CommandResult Handler(IEventMsg msg);
        void ErrorHandler(IEventMsg msg, Exception ex);
    }
}
