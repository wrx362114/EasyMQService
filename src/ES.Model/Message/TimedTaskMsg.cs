using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.Model.Message
{
    /// <summary>
    /// 创建定时任务消息实体
    /// </summary>
    public class TimedTaskMsg : IEventMsg
    {
        /// <summary>
        /// 制定当前消息类型
        /// </summary>
        public EventType Event { get { return EventType.TimedTask; } }
        /// <summary>
        /// 已重试次数
        /// </summary>
        public int RetryCount { get; set; }
        /// <summary>
        /// 定时循环字符串
        /// 为空时使用starttime为触发时间.执行一次性任务
        /// </summary>
        public string Cron { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 要发送的消息类型完整名称
        /// </summary>
        public string MsgTypeFullName { get; set; }
        /// <summary>
        /// 要发送的消息实体json序列化
        /// </summary>
        public string MsgJson { get; set; }
    }
}
