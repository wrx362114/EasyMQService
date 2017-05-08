using ES.Common.Extend;
using ES.Framework.Core;
using ES.Model.Message;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text; 

namespace ES.Quartz
{
    public class SendEventMsgJob : IJob
    {
        private Logger Log;
        public SendEventMsgJob(Logger log)
        {
            Log = log;
        }
        public void Execute(IJobExecutionContext context)
        {
            Log.Info($"[SendEventMsgJob] 开始执行定时发送消息任务");
            var msgjson = context.MergedJobDataMap["MsgJson"].ToString();
            if (string.IsNullOrEmpty(msgjson))
            {
                Log.Warn("[SendEventMsgJob] 任务数据中没有配置要处理的消息体json");
                return;
            }
            var msg = msgjson.FromJson<TimedTaskMsg>();
            if (msg == null)
            {
                Log.Warn("[SendEventMsgJob] 任务数据要处理的消息为空");
                return;
            }
            var assembly = Assembly.GetAssembly(typeof(IEventMsg));
            var type = assembly.GetType(msg.MsgTypeFullName);

            var objmsg = Newtonsoft.Json.JsonConvert.DeserializeObject(msg.MsgJson, type) as IEventMsg;
            if (objmsg == null)
            {
                Log.Warn($"[SendEventMsgJob] 任务:{msg.TaskId}要处理的消息为空");
                return;
            }
            Log.Warn($"[SendEventMsgJob] 任务:{objmsg.Event.ToString()}|{msg.TaskId}正在发送消息.计划时间:{msg.StartTime}");
            objmsg.Publish();
        }
    }
}