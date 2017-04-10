using System;
using Microsoft.Owin;
using Owin;
using Hangfire; 
using ES.Framework.Core;

[assembly: OwinStartup(typeof(ES.Framework.Startup))]
namespace ES.Framework
{
    public class Startup
    {
        private Logger Log;
        public Startup(Logger log)
        {
            Log = log;
        }
        public void Configuration(IAppBuilder app)
        { 
            GlobalConfiguration.Configuration.UseSqlServerStorage("HangfireDb");
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            var config = new System.Web.Http.HttpConfiguration();
            config.DependencyResolver = new NinjectDependencyResolver(Ninject.NinjectBuilderConfigurator.Kernel);
            app.UseWebApi(config);
        }
        private void SetRoute()
        {

        }
    }
}
