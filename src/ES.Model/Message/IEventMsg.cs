using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.Model.Message
{
    /// <summary>
    /// 事件消息实体基类
    /// </summary>
    public interface IEventMsg
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        EventType Event { get; }
        /// <summary>
        /// 已重试次数
        /// </summary>
        int RetryCount { get; set; }
    }
}
