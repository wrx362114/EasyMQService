using Ninject;
using Ninject.Syntax;
using Quartz;
using Quartz.Spi;

namespace ES.Framework.Quartz
{
    public class NinjectJobFactory : IJobFactory
    {
        private readonly IResolutionRoot resolutionRoot;

        public NinjectJobFactory(IResolutionRoot resolutionRoot)
        {
            this.resolutionRoot = resolutionRoot;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)this.resolutionRoot.Get(bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
            this.resolutionRoot.Release(job);
        }
    }
}
