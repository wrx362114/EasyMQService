using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Logging;
using EasyMQ.Framework.Ninject;
using EasyMQ.Framework.Core;
using EasyMQ.Framework.Common.Extension;

namespace EasyMQ.Service
{
    class Program
    {
        static void Main(string[] args)
        { 
            HostFactory.New(c =>
            {
                c.UseLog4Net("log4net.config", true);
                var log = HostLogger.Get(typeof(EasyMQ.Service.Program));
                log.Info("[Program] 已读取log4net配置");
                c.StartManually();
                c.StartAutomaticallyDelayed();
                c.UseNinject();
                c.RunAsNetworkService();
                log.Info("[Program] 开始生成服务");
                c.Service<CoreService>(s =>
                {
                    s.ConstructUsingNinject();
                    s.WhenStarted((cs, hc) => cs.Start(hc));
                    s.WhenStopped((cs, hc) => cs.Stop(hc));
                });
                c.SetDescription("ServiceDescription".GetAppSetting() ?? "简单服务框架");
                c.SetDisplayName("ServiceDisplayName".GetAppSetting() ?? "简单服务框架");
                c.SetServiceName("ServiceName".GetAppSetting() ?? "简单服务框架");
            }).Run();
        }
    }
}
