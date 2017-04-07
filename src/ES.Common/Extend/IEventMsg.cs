using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace ES.Common.Extend
{
    /// <summary>
    /// 消息基类扩展方法
    /// </summary>
    public static class IEventMsgExtend
    {
        private static object _sync = new object();
        private static IBus bus;
        private static IBus Bus
        {
            get
            {
                if (bus == null)
                {
                    lock (_sync)
                    {
                        if (bus == null)
                        {
                            if (string.IsNullOrEmpty("RabbitServerUrl".GetConfig()))
                            {
                                throw new Exception("rabbit消息队列服务器连接字符串没有配置");
                            }
                            bus = RabbitHutch.CreateBus("RabbitServerUrl".GetConfig());
                        }
                    }
                }
                return bus;
            }
        }
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static async Task PublishAsync(this IEventMsg msg)
        {
            await Bus.PublishAsync(msg);
        }
    }
}
