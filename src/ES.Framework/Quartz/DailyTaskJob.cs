using ES.Common.Extend;
using ES.Model.Message;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text; 

namespace ES.Framework.Quartz
{
    public class DailyTaskJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            new DailyTasksMsg
            {
                Date=DateTime.Now.Date
            }.Publish();
        }
    }
}