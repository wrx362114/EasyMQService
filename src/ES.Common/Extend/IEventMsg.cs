using EasyNetQ;
using ES.Model.Message;
using System;
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
        public static IBus Bus
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
        {//TODO:优化并发性能.
            lock (_sync)
            {
                Bus.PublishAsync(msg).Wait();
            }
        }
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static void Publish(this IEventMsg msg)
        {
            lock (_sync)
            {
                Bus.Publish(msg);
            }
        }
        public static TimedTaskMsg SetMsg(this TimedTaskMsg msg, IEventMsg value)
        {
            msg.MsgTypeFullName = value.GetType().FullName;
            msg.MsgJson = value.ToJson();
            return msg;
        }
    }
}
