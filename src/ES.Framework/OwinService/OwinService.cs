using Microsoft.Owin.Hosting;
using System;
using ES.Framework.Core;
using ES.Model.Message;
using ES.Common.Extend;
using Hangfire;
using Owin;
using CrystalQuartz.Owin;
using Quartz;

namespace ES.Framework
{
    public class OwinService : IService
    {
        private Logger Log;
        private IScheduler Scheduler;
        public OwinService(Logger log, IScheduler scheduler)
        {
            Log = log;
            Scheduler = scheduler;
        }
        private IDisposable _host;

        public string Name => "Web宿主服务";

        public bool Start()
        {
            if (typeof(Microsoft.Owin.Host.HttpListener.OwinServerFactory).FullName.Contains("没啥用"))
            {
                Log.Info("[OwinService] 解决Microsoft.Owin.Host.HttpListener不被复制问题");
            }
            Log.Info("[OwinService] 正在启动web服务");
            try
            {
                _host = WebApp.Start("ApiHost".GetConfig(), m =>
                {
                    Log.Info("[CrystalQuartz] 配置定时服务管理页面");
                    m.UseCrystalQuartz(Scheduler);
                });
            }
            catch (Exception ex)
            {
                Log.Error("[OwinService] 启动web服务失败->" + ex.ToString());
            }
            Log.Info("[OwinService] web服务已启动,正在监听:" + "ApiHost".GetConfig());
            return true;
        }

        public bool Stop()
        {
            _host.Dispose();
            return true;
        }
    }
}
