using ES.Common.Extend;
using ES.Framework.Core;
using ES.Framework.Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Logging;

namespace ES.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.New(c =>
            {
                c.UseLog4Net("log4net.config", true);
                var log = HostLogger.Get("Default");
                log.Info("[Service] 已读取log4net配置");
                c.StartManually();
                c.StartAutomaticallyDelayed();
                c.UseNinject();
                c.RunAsNetworkService();
                log.Info("[Service] 开始生成服务");
                c.Service<CoreService>(s =>
                {
                    s.ConstructUsingNinject();
                    s.WhenStarted((cs, hc) => cs.Start(hc));
                    s.WhenStopped((cs, hc) => cs.Stop(hc));
                });
                c.SetDescription("ServiceDescription".GetConfig() ?? "简单服务框架");
                c.SetDisplayName("ServiceDisplayName".GetConfig() ?? "简单服务框架");
                c.SetServiceName("ServiceName".GetConfig() ?? "简单服务框架");
            }).Run();
        }
    }
}
