using ES.Framework.Core;
using ES.Framework.Quartz;
using Quartz;

namespace ES.RabbitMQ
{
    public class SchedulerService : IService
    {
        private Logger Log;
        private IScheduler Scheduler;
        public SchedulerService(Logger log, IScheduler scheduler)
        {

            Log = log;
            Scheduler = scheduler;
        }

        public string Name => "任务调度服务";

        public bool Start()
        {
            if (Scheduler.GetJobDetail(new JobKey("DailyTaskJob")) == null)
            {
                var job = JobBuilder
                    .Create<DailyTaskJob>()
                    .WithIdentity("DailyTaskJob")
                    .Build();
                var trigger = TriggerBuilder
                    .Create()
                    .WithCronSchedule("0 0 1 * * ?")
                    .Build();
                Scheduler.ScheduleJob(job, trigger);
            }
            Log.Info("[SchedulerService] 将每日任务添加到调度框架");
            Scheduler.Start();
            return true;
        }

        public bool Stop()
        {
            Scheduler.Shutdown(false);
            return true;
        }
    }
}
