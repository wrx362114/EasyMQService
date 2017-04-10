using Hangfire;
using System;
using System.Reflection;
using ES.Framework.Core;
using ES.Model.Message;
using ES.Model;
using ES.Common.Extend;

namespace ES.Framework.RabbitMQ.EventHandler
{
    /// <summary>
    /// 定时任务处理
    /// </summary>
    public class TimedTaskHandler : BaseEvent<TimedTaskMsg>
    {
        public TimedTaskHandler(Logger log) : base(log)
        {
        }

        public override EventType Event { get { return EventType.TimedTask; } }

        public override ResultModel Handler(TimedTaskMsg msg)
        {
            var assembly = Assembly.GetAssembly(typeof(IEventMsg));
            var sendmsg = assembly.CreateInstance(msg.MsgTypeFullName).ToJson();
            if (string.IsNullOrWhiteSpace(msg.Cron))
            {
                BackgroundJob.Schedule(() => SendMsg(msg), msg.StartTime - DateTime.Now);
            }
            else
            {
                RecurringJob.AddOrUpdate(() => SendMsg(msg), msg.Cron);
            }
            return ResultModel.Ok();
        }

        public static void SendMsg(TimedTaskMsg msg)
        {
            var assembly = Assembly.GetAssembly(typeof(IEventMsg));
            var type = assembly.CreateInstance(msg.MsgTypeFullName);
            var objmsg = Newtonsoft.Json.JsonConvert.DeserializeObject(msg.MsgJson, type.GetType()) as IEventMsg;
            objmsg.PublishAsync();
        }
    }
}
