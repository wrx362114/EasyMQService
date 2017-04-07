using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.Model.Message
{
    /// <summary>
    /// 系统所有事件类型枚举
    /// </summary>
    public enum EventType
    {
        #region 基础事件 1-99

        /// <summary>
        /// 定时任务,
        /// 定时发送消息触发指定事件
        /// </summary>
        TimedTask = 1,
        /// <summary> 每日任务 </summary>
        DailyTasks

        #endregion
    } 
}
