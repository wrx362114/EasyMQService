using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.Model.Message
{
    /// <summary>
    /// 每日任务消息
    /// </summary>
    public class DailyTasksMsg : IEventMsg
    {
        /// <summary>
        /// 当前日期
        /// </summary>
        public DateTime Date { get; set; }

        public EventType Event { get { return EventType.DailyTasks; } }

        public int RetryCount { get; set; }
    }
}
