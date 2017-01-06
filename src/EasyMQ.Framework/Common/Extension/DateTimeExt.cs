using EasyMQ.Framework.Common.Cron;
using System;

namespace EasyMQ.Framework.Common.Extension
{
	public static class DateTimeExt
	{
		public static bool InCron(this DateTime time, string cron)
		{
			return new CronExpression(cron).TimeInCron(time);
		}
	}
}
