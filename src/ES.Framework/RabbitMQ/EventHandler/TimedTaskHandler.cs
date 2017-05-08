using Hangfire;
using System;
using System.Reflection;
using ES.Framework.Core;
using ES.Model.Message;
using ES.Model;
using ES.Common.Extend;
using Quartz;
using ES.Quartz;
using EasyNetQ;

namespace ES.Framework.RabbitMQ.EventHandler
{
    /// <summary>
    /// 定时任务处理
    /// </summary>
    public class TimedTaskHandler : BaseEvent<TimedTaskMsg>
    {
        private IBus Bus;
        private IScheduler Scheduler;
        public TimedTaskHandler(Logger log, IBus bus, IScheduler schedule) : base(log)
        {
            Bus = bus;
            Scheduler = schedule;
        }

        public override EventType Event { get { return EventType.TimedTask; } }

        public override ResultModel Handler(TimedTaskMsg msg)
        {
            var assembly = Assembly.GetAssembly(typeof(IEventMsg));
            var type = assembly.GetType(msg.MsgTypeFullName);
            var objmsg = Newtonsoft.Json.JsonConvert.DeserializeObject(msg.MsgJson, type) as IEventMsg;
            if (objmsg == null)
            {
                Log.Warn("[TimedTaskHandler] 消息无法解析,消息体为:" + msg.MsgJson);
                return ResultModel.Ok();
            }
            if (msg.StartTime <= DateTime.Now)
            {
                Log.Info($"[TimedTaskHandler] 触发定时任务{objmsg.Event.ToString()},消息配置在:{msg.StartTime.ToString()}时触发");
                objmsg.Publish();
                return ResultModel.Ok();
            }
            if (string.IsNullOrEmpty(msg.TaskId))
            {
                msg.TaskId = objmsg.Event.ToString() + "_" + Guid.NewGuid().ToString();
            }

            var job = Scheduler.GetJobDetail(new JobKey(msg.TaskId));
            if (job != null)
            {
                Log.Info($"[TimedTaskHandler] 该任务已存在:{objmsg.Event.ToString()} Id:{msg.TaskId},将被删除");
                Scheduler.DeleteJob(new JobKey(msg.TaskId));
            }
            job = JobBuilder
                  .Create<SendEventMsgJob>()
                  .WithIdentity(msg.TaskId)
                  .UsingJobData("MsgJson", msg.ToJson())
                  .Build();
            var trigger = TriggerBuilder
                     .Create()
                     .StartAt(msg.StartTime)
                     .ForJob(job)
                     .Build();
            Scheduler.ScheduleJob(job, trigger);
            Log.Info($"[TimedTaskHandler] 添加一个任务:{objmsg.Event.ToString()} Id:{msg.TaskId},在:{msg.StartTime.ToString()}时后启动");

            return ResultModel.Ok();
        }
    }
}
