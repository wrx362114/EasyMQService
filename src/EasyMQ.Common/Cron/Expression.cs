using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMQ.Common.Cron
{
	/// <summary>
	/// 时间跨度表达式
	/// 只支持 ‘数字’，‘*’，‘-’，‘,’，‘ ’
	/// </summary>
	public class CronExpression
	{
		private const string EnableChar = "0123456789,*- ";
		public CronExpression(string cron)
		{
			if (cron.Any(m => !EnableChar.Contains(m)))
			{
				throw new FormatException("Cron表达式包含不支持的字符:{0}".FormatExt(cron));
			}
			#region 初始化

			if (Seconds == null)
			{
				Seconds = new List<int>();
			}
			if (Minutes == null)
			{
				Minutes = new List<int>();
			}
			if (Hours == null)
			{
				Hours = new List<int>();
			}
			if (DaysOfMonth == null)
			{
				DaysOfMonth = new List<int>();
			}
			if (Months == null)
			{
				Months = new List<int>();
			}
			if (DaysOfWeek == null)
			{
				DaysOfWeek = new List<int>();
			}
			if (Years == null)
			{
				Years = new List<int>();
			}
			#endregion

			int exprOn = (int)CronFieldIndex.Second;
			string[] exprsTok = cron.Trim().Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);


			foreach (string exprTok in exprsTok)
			{
				string expr = exprTok.Trim();

				if (expr.Length == 0)
				{
					continue;
				}
				string[] vTok = expr.Split(',');
				foreach (string v in vTok)
				{
					var v1 = v.TrimAll();
					Add2List((CronFieldIndex)exprOn, v1);
					exprOn++;
				}

			}
		}
		private void Add2List(CronFieldIndex index, string value)
		{

			switch (index)
			{
				case CronFieldIndex.Second:
					if (value == "*")
					{
						60.For(n => Seconds.Add(n));
					}
					else if (value.Contains('-'))
					{
						var values = value.Split('-').Select(m => m.ToInt32()).ToArray();
						(values[1] - values[0] + 1).For(m => Seconds.Add(values[0] + m));
					}
					else
					{
						Seconds.Add(value.ToInt32());
					}
					break;
				case CronFieldIndex.Minute:
					if (value == "*")
					{
						60.For(n => Minutes.Add(n));
					}
					else if (value.Contains('-'))
					{
						var values = value.Split('-').Select(m => m.ToInt32()).ToArray();
						(values[1] - values[0] + 1).For(m => Minutes.Add(values[0] + m));
					}
					else
					{
						Minutes.Add(value.ToInt32());
					}
					break;
				case CronFieldIndex.Hour:
					if (value == "*")
					{
						24.For(n => Hours.Add(n));
					}
					else if (value.Contains('-'))
					{
						var values = value.Split('-').Select(m => m.ToInt32()).ToArray();
						(values[1] - values[0] + 1).For(m => Hours.Add(values[0] + m));
					}
					else
					{
						Hours.Add(value.ToInt32());
					}
					break;
				case CronFieldIndex.DayOfMonth:
					if (value == "*")
					{
						31.For(n => DaysOfMonth.Add(n + 1));
					}
					else if (value.Contains('-'))
					{
						var values = value.Split('-').Select(m => m.ToInt32()).ToArray();
						(values[1] - values[0] + 1).For(m => DaysOfMonth.Add(values[0] + m));
					}
					else
					{
						DaysOfMonth.Add(value.ToInt32());
					}
					break;
				case CronFieldIndex.Month:
					if (value == "*")
					{
						12.For(n => Months.Add(n + 1));
					}
					else if (value.Contains('-'))
					{
						var values = value.Split('-').Select(m => m.ToInt32()).ToArray();
						(values[1] - values[0] + 1).For(m => Months.Add(values[0] + m));
					}
					else
					{
						Months.Add(value.ToInt32());
					}
					break;
				case CronFieldIndex.DayOfWeek:
					if (value == "*")
					{
						7.For(n => DaysOfWeek.Add(n + 1));
					}
					else if (value.Contains('-'))
					{
						var values = value.Split('-').Select(m => m.ToInt32()).ToArray();
						(values[1] - values[0] + 1).For(m => DaysOfWeek.Add(values[0] + m));
					}
					else
					{
						DaysOfWeek.Add(value.ToInt32());
					}
					break;
				case CronFieldIndex.Year:
					if (value == "*")
					{
						Years.Add(DateTime.Now.Year);
					}
					else if (value.Contains('-'))
					{
						var values = value.Split('-').Select(m => m.ToInt32()).ToArray();
						(values[1] - values[0] + 1).For(m => Years.Add(values[0] + m));
					}
					else
					{
						Years.Add(value.ToInt32());
					}
					break;
				default:
					throw new AggregateException("输入参数错误");
			}
		}

		private List<int> Seconds { get; set; }
		private List<int> Minutes { get; set; }
		private List<int> Hours { get; set; }
		private List<int> DaysOfMonth { get; set; }
		private List<int> Months { get; set; }
		private List<int> DaysOfWeek { get; set; }
		private List<int> Years { get; set; }
		public bool TimeInCron(DateTime time)
		{
			//1.秒
			//2.分
			//3.小时
			//4.日
			//5.月
			//6.星期几
			//7.年
			return Seconds.Contains(time.Second)
				&& Minutes.Contains(time.Minute)
				&& Hours.Contains(time.Hour)
				&& DaysOfMonth.Contains(time.Day)
				&& DaysOfWeek.Contains((int)time.DayOfWeek)
				&& Months.Contains(time.Month)
				&& (!Years.Any() || Years.Contains(time.Year));
		}
	}
	public enum CronFieldIndex
	{
		/// <summary> 秒	0-59	, - * / </summary>
		Second,
		/// <summary> 分	0-59	, - * / </summary>
		Minute,
		/// <summary> 小时	0-23	, - * / </summary>
		Hour,
		/// <summary> 日	1-31	, - * ? / L W C </summary>
		DayOfMonth,
		/// <summary> 月	1-12 	, - * / </summary>
		Month,
		/// <summary> 周几	1-7     , - * ? / L C # </summary>
		DayOfWeek,
		/// <summary> 年 (可选字段)	空 或1970-2099	, - * / </summary>
		Year
	}
}
