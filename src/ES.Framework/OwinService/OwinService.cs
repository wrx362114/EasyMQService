using Microsoft.Owin.Hosting;
using System;
using ES.Framework.Core;
using ES.Model.Message;
using ES.Common.Extend;

namespace ES.Framework
{
    public class OwinService : IService
    {
        private Logger Log;
        public OwinService(Logger log)
        {
            Log = log;
        }
        private string endpoint = "http://localhost:8081";
        private IDisposable _host;
        public bool Start()
        {
            if (typeof(Microsoft.Owin.Host.HttpListener.OwinServerFactory).FullName.Contains("没啥用"))
            {
                Log.Info("[OwinService] 解决Microsoft.Owin.Host.HttpListener不被复制问题");
            }
            Log.Info("[OwinService] 正在启动web服务");
            try
            {
                _host = WebApp.Start<Startup>(new StartOptions
                {
                    Port=8081
                });
                Hangfire.RecurringJob
                    .AddOrUpdate("DailyTasks", () => new DailyTasksMsg { Date = DateTime.Now.Date }.PublishAsync().Wait(), "0 1 * * *");
                Log.Info("[OwinService.DailyTasks] 初始化每日任务");
            }
            catch (Exception ex)
            {
                Log.Error("[OwinService] 启动web服务失败->" + ex.ToString());
            }
            Log.Info("[OwinService] web服务已启动,正在监听:" + endpoint);
            return true;
        }

        public bool Stop()
        {
            _host.Dispose();
            return true;
        }
    }
}
